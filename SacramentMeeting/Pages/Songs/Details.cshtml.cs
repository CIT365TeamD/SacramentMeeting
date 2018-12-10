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
    public class DetailsModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public DetailsModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        public Song Song { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Song = await _context.Song
                .Include(s => s.SongSelections)
                .ThenInclude(ss => ss.Meeting)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.SongID == id);

            if (Song == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
