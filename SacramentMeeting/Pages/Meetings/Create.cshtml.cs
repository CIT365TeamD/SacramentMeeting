﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SacramentMeeting.Models;

namespace SacramentMeeting.Pages.Meetings
{
    [BindProperties]
    public class CreateModel : MeetingPrayersPageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public CreateModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
           
            PopulateBishopricSL(_context);
            PopulatePrayersSLI(_context);
            PopulateSongsSLI(_context);
            
            return Page();
            
            
        }

        [BindProperty]
        public Meeting Meeting { get; set; }
        public string OpeningPrayer { get; set; }
        public string ClosingPrayer { get; set; }
        public string OpeningSong { get; set; }
        public string SacramentSong { get; set; }
        public string ClosingSong { get; set; }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateBishopricSL(_context);
                PopulatePrayersSLI(_context);
                PopulateSongsSLI(_context);

                return Page();
            }

            var newMeeting = new Meeting();
            // Add prayers
            if (OpeningPrayer != null || ClosingPrayer != null)
            {
                newMeeting.Prayers = new List<Prayer>();
                if (OpeningPrayer != null)
                {
                    var prayerToAddO = new Prayer
                    {
                        MemberID = int.Parse(OpeningPrayer),
                        Schedule = PrayerPosition.Opening
                    };
                    newMeeting.Prayers.Add(prayerToAddO);
                }
                if (ClosingPrayer != null)
                {
                    var prayerToAddC = new Prayer
                    {
                        MemberID = int.Parse(ClosingPrayer),
                        Schedule = PrayerPosition.Closing
                    };
                    newMeeting.Prayers.Add(prayerToAddC);
                }
            }

            // add songs
            if (OpeningSong != null || SacramentSong != null || ClosingSong != null)
            {
                newMeeting.SongSelections = new List<SongSelection>();
                if (OpeningSong != null)
                {
                    var songToAddO = new SongSelection
                    {
                        SongID = int.Parse(OpeningSong),
                        Schedule = SongPosition.Opening
                    };
                    newMeeting.SongSelections.Add(songToAddO);
                }
                if (SacramentSong != null)
                {
                    var songToAddS = new SongSelection
                    {
                        SongID = int.Parse(SacramentSong),
                        Schedule = SongPosition.Sacrament
                    };
                    newMeeting.SongSelections.Add(songToAddS);
                }
                if (ClosingSong != null)
                {
                    var songToAddC = new SongSelection
                    {
                        SongID = int.Parse(ClosingSong),
                        Schedule = SongPosition.Closing
                    };
                    newMeeting.SongSelections.Add(songToAddC);
                }
            }
          
            try
            {

                if (await TryUpdateModelAsync<Meeting>(
                    newMeeting,
                    "Meeting",
                    i => i.MeetingDate,
                    i => i.CallingID))
                {
                    _context.Meeting.Add(newMeeting);
                    await _context.SaveChangesAsync();
                    //return RedirectToPage("./Index");
                    return RedirectToPage("../Talks/Create", new {id = newMeeting.MeetingID });
            }
            }
            catch (Exception)
            {

                PopulateBishopricSL(_context, newMeeting.CallingID);
                PopulateSongsSLI(_context, newMeeting);
                PopulatePrayersSLI(_context, newMeeting);

                return RedirectToPage("./Create");
            }

            return RedirectToPage("./Index");
        }
    }
}