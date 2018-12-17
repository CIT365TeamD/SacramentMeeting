using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SacramentMeeting.Models;

namespace SacramentMeeting.Pages.Songs
{
    public class DeleteModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public DeleteModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Song Song { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Song = await _context.Song
                .Include(m => m.SongSelections)
                    .ThenInclude(m => m.Meeting)
                .FirstOrDefaultAsync(m => m.SongID == id);

            if (Song == null)
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

            Song = await _context.Song
                .Include(m => m.SongSelections)
                    .ThenInclude(m => m.Meeting)
                .FirstOrDefaultAsync(m => m.SongID == id);

            if (Song != null)
            {
                foreach (var item in Song.SongSelections)
                {
                    _context.SongSelection.Remove(item);
                }
                _context.Song.Remove(Song);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
