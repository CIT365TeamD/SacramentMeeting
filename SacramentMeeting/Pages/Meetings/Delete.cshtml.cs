using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SacramentMeeting.Models;

namespace SacramentMeeting.Pages.Meetings
{
    public class DeleteModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public DeleteModel(SacramentMeeting.Models.SacramentMeetingContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
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

            if (Meeting != null)
            {
                // remove the talks
                foreach (var item in Meeting.Talks)
                {
                    _context.Talk.Remove(item);
                }
                // remove the prayers
                foreach (var item in Meeting.Prayers)
                {
                    _context.Prayer.Remove(item);
                }
                // remove the songsSelections
                foreach (var item in Meeting.SongSelections)
                {
                    _context.SongSelection.Remove(item);
                }
                _context.Meeting.Remove(Meeting);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
