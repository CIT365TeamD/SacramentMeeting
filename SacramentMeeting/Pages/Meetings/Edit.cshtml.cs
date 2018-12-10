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
    public class EditModel : PageModel
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
                .Include(m => m.Calling).FirstOrDefaultAsync(m => m.MeetingID == id);

            if (Meeting == null)
            {
                return NotFound();
            }
           ViewData["CallingID"] = new SelectList(_context.Calling, "CallingID", "Title");
            ViewData["OpeningSong"] = new SelectList(_context.Song, "SongID", "Title");
            ViewData["ClosingSong"] = new SelectList(_context.Song, "SongID", "Title");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Meeting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingExists(Meeting.MeetingID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MeetingExists(int id)
        {
            return _context.Meeting.Any(e => e.MeetingID == id);
        }
    }
}
