using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentMeeting.Models;

namespace SacramentMeeting.Pages.Meetings
{
    public class EditModel : MeetingPrayersPageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public EditModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Meeting Meeting { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Meeting = await _context.Meeting
                .Include(m => m.Calling)
                    .ThenInclude(m => m.CurrentCallings)
                        .ThenInclude(m => m.Member)
                .Include(m => m.Prayers)
                   .ThenInclude(m => m.Member)
                .Include(m => m.Talks)
                    .ThenInclude(m => m.Member)
                .Include(m => m.SongSelections)
                    .ThenInclude(m => m.Song)
                .FirstOrDefaultAsync(m => m.MeetingID == id);

            if (Meeting == null)
            {
                return NotFound();
            }
            PopulateBishopricSL(_context, Meeting.CallingID);
            PopulatePrayersSLI(_context, Meeting);
            PopulateSongsSLI(_context, Meeting);

            return Page();
        }

        [BindProperty]
        public string OpeningPrayer { get; set; }
        [BindProperty]
        public string ClosingPrayer { get; set; }
        [BindProperty]
        public string OpeningSong { get; set; }
        [BindProperty]
        public string SacramentSong { get; set; }
        [BindProperty]
        public string ClosingSong { get; set; }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                PopulateBishopricSL(_context, Meeting.Calling);
                PopulatePrayersSLI(_context, Meeting);
                PopulateSongsSLI(_context, Meeting);
                return Page();
            }

           // get meeting that needs to be updated
            var meetingToUpdate = await _context.Meeting
                .Include(m => m.SongSelections)
                    .ThenInclude(m => m.Song)
                .Include(m => m.Prayers)
                    .ThenInclude(m => m.Member)
            .FirstOrDefaultAsync(s => s.MeetingID == id);

            // Add or remove songs to update Meeting.SongSelection
            UpdateSong(_context, meetingToUpdate, meetingToUpdate.SongSelections, SongPosition.Opening, OpeningSong );
            UpdateSong(_context, meetingToUpdate, meetingToUpdate.SongSelections, SongPosition.Sacrament, SacramentSong);
            UpdateSong(_context, meetingToUpdate, meetingToUpdate.SongSelections, SongPosition.Closing, ClosingSong);

            // Add or remove prayers to update Meeting.Prayer
            UpdatePrayer(_context, meetingToUpdate, meetingToUpdate.Prayers, PrayerPosition.Opening, OpeningPrayer);
            UpdatePrayer(_context, meetingToUpdate, meetingToUpdate.Prayers, PrayerPosition.Closing, ClosingPrayer);
            if (await TryUpdateModelAsync<Meeting>(
                meetingToUpdate,
                "meeting",
                 m => m.MeetingDate, m => m.CallingID))
            {
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("../Talks/Details", new { id = meetingToUpdate.MeetingID });
        }
    }
}
