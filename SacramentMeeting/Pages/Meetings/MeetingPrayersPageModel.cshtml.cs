using SacramentMeeting.Data;
using SacramentMeeting.Pages;
using SacramentMeeting.Models;
using SacramentMeeting.Models.SacramentViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SacramentMeeting.Pages.Meetings
{
    public class MeetingPrayersPageModel : PageModel
    {
        // NOT BEING USED. instantiate list of AssignedMemberPrayerData, which is viewModel. 
        //List of members assigned to pray
        public List<AssignedMemberPrayerData> AssignedMemberPrayerDataList;
        
        // method to populate prayers from meeting. Parameters are context, and meeting
        public void PopulateAssignedMemberPrayerData(SacramentMeetingContext context,
            Meeting meeting)
        {
            // Get whole member Table 
            var allMembers = context.Member;
            var MeetingPMembers = new HashSet<int>(
                meeting.Prayers.Select(p => p.Member.ID));
            AssignedMemberPrayerDataList = new List<AssignedMemberPrayerData>();
           
            foreach (var member in allMembers)
            {
                AssignedMemberPrayerDataList.Add(new AssignedMemberPrayerData
                {
                    MemberID = member.ID,
                    FullName = member.FullName,
                });
            }
            

        }

        // create dropdown list for members
        public SelectList MemberSL { get; set; }
        public void PopulateMemberDropDownList(
            SacramentMeetingContext _context, object selectedMember = null)
        {
            var membersQuery = from m in _context.Member
                               select m;
            MemberSL = new SelectList(
                membersQuery.AsNoTracking(), "ID", "FullName", selectedMember);
        }

        // Update List of prayers for meeting
        public void UpdateMeetingPrayerMembers(SacramentMeetingContext context,
            string[] SelectedPrayerMembers, Meeting meetingToUpdate)
        {
            if (SelectedPrayerMembers == null)
            {
                meetingToUpdate.Prayers = new List<Prayer>();
                return;
            }

            var selectedPrayerMembersHS = new HashSet<string>(SelectedPrayerMembers);
            var meetingPrayerMembers = new HashSet<int>
                (meetingToUpdate.Prayers.Select(p => p.Member.ID));
         
            foreach (var member in context.Member)
            {
                if (selectedPrayerMembersHS.Contains(member.ID.ToString()))
                {
                    if (!meetingPrayerMembers.Contains(member.ID))
                    {
                        meetingToUpdate.Prayers.Add(new Prayer
                        {
                            MeetingID = meetingToUpdate.MeetingID,
                            MemberID = member.ID,
                            
                        });
                    }       
                }
                else
                {
                    if (meetingPrayerMembers.Contains(member.ID))
                    {
                        Prayer prayerToRemove
                            = meetingToUpdate
                            .Prayers
                            .SingleOrDefault(i => i.MemberID == member.ID);
                        context.Remove(prayerToRemove);
                    }
                }
            }
        }

        // create dropdown list for just bishopric
        public SelectList BishopricCallingsSL { get; set; }
        

        public void PopulateBishopricDropdownList(SacramentMeetingContext _context,
            object selectedCalling = null)
        {
            var callingsQuery = from c in _context.Calling
                                where c.Organization == Organizations.Bishopric
                                select c;

            BishopricCallingsSL = new SelectList(callingsQuery.AsNoTracking(),
                "CallingID", "Title", selectedCalling);
        }

        // create dropdown list of songs
        public SelectList SongsSL { get; set; }
        public SelectList SacramentSongsSL { get; set; }
        public void PopulateSongsDropdownList(SacramentMeetingContext _context,
            object selectedSong = null)
        {
            var songsQuery = from s in _context.Song
                             select s;
            

            SongsSL = new SelectList(songsQuery.AsNoTracking(),
                "SongID", "Title", selectedSong);
        }
        public void PopulateSacramentSongsSL(SacramentMeetingContext _context,
            object selectedSong = null)
        {
            var sacSongsQuery = from s in _context.Song
                                where s.SongID > 168
                                where s.SongID < 197
                                select s;
            SacramentSongsSL = new SelectList(sacSongsQuery.AsNoTracking(),
                "SongID", "Title", selectedSong);
        }
        
    }
}
