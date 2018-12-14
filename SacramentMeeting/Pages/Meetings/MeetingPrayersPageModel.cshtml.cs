using SacramentMeeting.Data;
using SacramentMeeting.Pages;
using SacramentMeeting.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SacramentMeeting.Pages.Meetings
{
    public class MeetingPrayersPageModel : PageModel
    {
        public List<SelectListItem> OpeningPrayerSLI { get; set; }
        public List<SelectListItem> ClosingPrayerSLI { get; set; }
        public SelectList BishopricCallingsSL { get; set; }
        public List<SelectListItem> ClosingSongSLI { get; set; }
        public List<SelectListItem> SacramentSongSLI { get; set; }
        public List<SelectListItem> OpeningSongSLI { get; set; }

        // validate meeting input
        public string ValidateMeetingInput(string OpeningSong = null, string ClosingSong = null, string SacramentSong = null, 
            string OpeningPrayer = null, string ClosingPrayer = null)
        {
            string Message = "";
            if (OpeningSong != null)
            {
                bool res = int.TryParse(OpeningSong, out int num);
                if (res == false) { Message = "Choose valid Opening Song."; }
            }
            if (ClosingSong != null)
            {
                bool res = int.TryParse(ClosingSong, out int num);
                if (res == false) { Message = "Choose valid Closing Song."; }
            }
            if (SacramentSong != null)
            {
                bool res = int.TryParse(SacramentSong, out int num);
                if (res == false) { Message = "Choose valid Sacrament Song."; }
            }
            if (OpeningPrayer != null)
            {
                bool res = int.TryParse(OpeningPrayer, out int num);
                if (res == false) { Message = "Choose valid Opening Song."; }
            }
            if (ClosingPrayer != null)
            {
                bool res = int.TryParse(ClosingPrayer, out int num);
                if (res == false) { Message = "Choose valid Closing Prayer."; }
            }
                return Message;

        }
           
        

        
        // Populates all song dropdown lists
        public void PopulateSongsSLI(SacramentMeetingContext context,
            Meeting meeting = null)
        {
            // create a list of all of the songs
            IList<Song> songs = context.Song
                .AsNoTracking()
                .ToList();

            // Get SongID of Opening, Closing, and Sacrament song for this meeting.
            IEnumerable<int> OpeningSongID = null;
            IEnumerable<int> ClosingSongID = null;
            IEnumerable<int> SacramentSongID = null;
            if (meeting != null)
            {
                if (meeting.SongSelections != null)
                {


                    OpeningSongID = meeting.SongSelections
                                          .Where(c => c.Schedule == SongPosition.Opening)
                                          .Select(c => c.SongID);

                    ClosingSongID = meeting.SongSelections
                                           .Where(c => c.Schedule == SongPosition.Closing)
                                           .Select(c => c.SongID);

                    SacramentSongID = meeting.SongSelections
                                         .Where(c => c.Schedule == SongPosition.Sacrament)
                                         .Select(c => c.SongID);
                }
            }
            // Create List of Select List items. For each song in Song table, create select list item

            OpeningSongSLI = new List<SelectListItem>();
            {

                if (OpeningSongID != null)
                {
                    foreach (Song song in songs)
                    {
                        OpeningSongSLI.Add(new SelectListItem
                        {
                            Value = song.SongID.ToString(),
                            Text = song.Display,
                            // if the songID for the song matches the SongID of the opening song (in OpenSong), Selected will be marked true.
                            Selected = OpeningSongID.Contains(song.SongID)
                        });
                    }
                }
                else
                {
                    foreach (Song song in songs)
                    {
                        OpeningSongSLI.Add(new SelectListItem
                        {
                            Value = song.SongID.ToString(),
                            Text = song.Display,
                        });
                    }

                }
            }



            // Create List of Select List items. For each song in Song table, create select list item
            ClosingSongSLI = new List<SelectListItem>();
            {
                if (ClosingSongID != null)
                {
                    foreach (Song song in songs)
                    {
                        ClosingSongSLI.Add(new SelectListItem
                        {
                            Value = song.SongID.ToString(),
                            Text = song.Display,
                            // if the songID for the song matches the SongID of the closing song (in CloseSong), Selected will be marked true.
                            Selected = ClosingSongID.Contains(song.SongID)
                        });

                    }
                }
                else
                {
                    foreach (Song song in songs)
                    {
                        ClosingSongSLI.Add(new SelectListItem
                        {
                            Value = song.SongID.ToString(),
                            Text = song.Display,
                        });

                    }
                }
            }

            // change list to filter for just Sacrament Songs.
            var sacSongs = songs.Where(s => s.SongID > 168).Where(s => s.SongID < 197);

            // Create List of Select List items. For each song in  sacSongs, create select list item
            SacramentSongSLI = new List<SelectListItem>();
            {
                if (SacramentSongID != null)
                {
                    foreach (Song song in sacSongs)
                    {
                        SacramentSongSLI.Add(new SelectListItem
                        {
                            Value = song.SongID.ToString(),
                            Text = song.Display,
                            // if the songID for the song matches the SongID of the sacrament song (in SacSong), Selected will be marked true.
                            Selected = SacramentSongID.Contains(song.SongID)
                        });

                    }
                }
                else
                {
                    foreach (Song song in sacSongs)
                    {
                        SacramentSongSLI.Add(new SelectListItem
                        {
                            Value = song.SongID.ToString(),
                            Text = song.Display,
                        });

                    }

                }
            }

        }

        // create dropdown list for prayers
        public void PopulatePrayersSLI(SacramentMeetingContext context,
            Meeting meeting = null)
        {
            // create list of all members
            IList<Member> members = context.Member
                .AsNoTracking()
                .ToList();
            IEnumerable<int> OpeningPrayerID = null;
            IEnumerable<int> ClosingPrayerID = null;
            // get ids of opening and closing prayer for meeting
            if (meeting != null)
            {
                if (meeting.Prayers != null)
                {
                    OpeningPrayerID = meeting.Prayers
                                .Where(p => p.MeetingID == meeting.MeetingID)
                                .Where(p => p.Schedule == PrayerPosition.Opening)
                                .Select(p => p.MemberID);


                    ClosingPrayerID = meeting.Prayers
                                      .Where(p => p.MeetingID == meeting.MeetingID)
                                      .Where(p => p.Schedule == PrayerPosition.Closing)
                                      .Select(p => p.MemberID);
                }
            }
            // create new select item list

            OpeningPrayerSLI = new List<SelectListItem>();
            {
                if (OpeningPrayerID != null)
                {
                    foreach (Member member in members)
                    {
                        OpeningPrayerSLI.Add(
                            new SelectListItem
                            {
                                Value = member.ID.ToString(),
                                Text = member.FullName,
                                Selected = OpeningPrayerID.Contains(member.ID)
                            });
                    }
                }
                else
                {
                    foreach (Member member in members)
                    {
                        OpeningPrayerSLI.Add(
                            new SelectListItem
                            {
                                Value = member.ID.ToString(),
                                Text = member.FullName
                            });
                    }
                }



                ClosingPrayerSLI = new List<SelectListItem>();
                {
                    if (ClosingPrayerID != null)
                    {
                        foreach (Member member in members)
                        {
                            ClosingPrayerSLI.Add(
                                new SelectListItem
                                {
                                    Value = member.ID.ToString(),
                                    Text = member.FullName,
                                    Selected = ClosingPrayerID.Contains(member.ID)
                                });
                        }

                    }
                    else
                    {
                        foreach (Member member in members)
                        {
                            ClosingPrayerSLI.Add(
                                new SelectListItem
                                {
                                    Value = member.ID.ToString(),
                                    Text = member.FullName
                                });
                        }
                    }

                }
            }

        }


            // create dropdown list for just bishopric
        public void PopulateBishopricSL(SacramentMeetingContext _context,
            object selectedCalling = null)
        {
            var callingsQuery = from c in _context.Calling
                                where c.Organization == Organizations.Bishopric
                                select c;

            BishopricCallingsSL = new SelectList(callingsQuery.AsNoTracking(),
                "CallingID", "Title", selectedCalling);

        }

        

            // update each song
            public void UpdateSong(SacramentMeetingContext context, Meeting meetingToUpdate,
                ICollection<SongSelection> songSelection, SongPosition schedule, string songID = null)
        {
            
            if (songID == null) // if songID is null, need to delete song if there is one in songPosition.
            {
                foreach (var item in songSelection) // check songPosition in songSelections. 
                {
                    if (item.Schedule.Equals(schedule)) // if there is a song in songPosition, delete it
                    {
                        SongSelection songToRemove
                            = meetingToUpdate
                            .SongSelections
                            .SingleOrDefault(i => i.SongID == item.SongID && i.Schedule == schedule);
                        context.Remove(songToRemove);
                        songSelection.Remove(songToRemove);
                        return;
                    } // if there is no song, do nothing because it is already empty
                }
            }
            else // if songID is not empty
            {
                int songIDInt = int.Parse(songID); // change songID to int
                int count = 0; // count to see if songPosition is empty
                foreach (var item in songSelection) // check each songSelection in meeting for correct songPosition
                {
                    if (item.Schedule.Equals(schedule))
                    {
                        count++; // there is song in songPosition   
                        

                        if (songIDInt != item.SongID) // if wrong song is in songPosition
                        {
                            SongSelection songToRemove // remove wrong song
                                = meetingToUpdate
                                .SongSelections
                                .SingleOrDefault(i => i.SongID == item.SongID && i.Schedule == schedule);
                            context.Remove(songToRemove);
                            songSelection.Remove(songToRemove);

                            meetingToUpdate.SongSelections.Add( // add the correct song
                                new SongSelection
                                {
                                    SongID = songIDInt,
                                    Schedule = schedule
                                });
                            return;
                        }

                    }
                   
                }
                if (count == 0) // the song position is empty
                {
                    meetingToUpdate.SongSelections.Add(
                        new SongSelection
                        {
                            SongID = songIDInt,
                            Schedule = schedule
                        });
                }
            }
        }

        // update each prayer
        public void UpdatePrayer(SacramentMeetingContext context, Meeting meetingToUpdate,
                ICollection<Prayer> prayers, PrayerPosition schedule, string memberID = null)
        {

            if (memberID == null) // if memberID is null, need to delete song if there is one in PrayerPosition.
            {
                foreach (var item in prayers) // check prayerPosition in prayers. 
                {
                    if (item.Schedule.Equals(schedule)) // if there is a prayer in the prayerPosition, delete it
                    {
                        Prayer prayerToRemove
                            = meetingToUpdate
                            .Prayers
                            .SingleOrDefault(i => i.MemberID == item.MemberID && i.Schedule == schedule);
                        context.Remove(prayerToRemove);
                        prayers.Remove(prayerToRemove);
                        return;
                    } // if there is no prayer, do nothing because it is already empty
                }
            }
            else // if memberID is not empty
            {
                int memberIDInt = int.Parse(memberID); // change songID to int
                int count = 0; // count to see if songPosition is empty
                foreach (var item in prayers) // check each prayer in meeting for correct prayerPosition
                {
                    if (item.Schedule.Equals(schedule))
                    {
                        count++; // there is prayer in prayerPosition   


                        if (memberIDInt != item.MemberID) // if wrong song is in prayerPosition
                        {
                            Prayer prayerToRemove // remove wrong prayer
                                = meetingToUpdate
                                .Prayers
                                .SingleOrDefault(i => i.MemberID == item.MemberID && i.Schedule == schedule);
                            context.Remove(prayerToRemove);
                            prayers.Remove(prayerToRemove);
                        

                            meetingToUpdate.Prayers.Add( // add the correct prayer
                                new Prayer
                                {
                                    MemberID = memberIDInt,
                                    Schedule = schedule
                                });
                            return;
                        }

                    }
                    
                }
                if (count == 0) // the prayerposition is empty
                {
                    meetingToUpdate.Prayers.Add(
                        new Prayer
                        {
                            MemberID = memberIDInt,
                            Schedule = schedule
                        });
                }
            }
        }

       


        

        
    }
}
    
