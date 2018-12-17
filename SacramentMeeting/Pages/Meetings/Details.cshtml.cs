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
    public class DetailsModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public DetailsModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        public Meeting Meeting { get; set; }
        public List<CurrentCalling> Callings { get; set; }

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
            Callings = await _context.CurrentCalling
                .Include(m => m.Calling)
                .Include(m => m.Member)
                .AsNoTracking()
                .ToListAsync();
            return Page();
        }
    }
}
