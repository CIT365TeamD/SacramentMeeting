using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SacramentMeeting.Models;

namespace SacramentMeeting.Pages.Current
{
    public class DetailsModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public DetailsModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        public CurrentCalling CurrentCalling { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CurrentCalling = await _context.CurrentCalling
                .Include(c => c.Calling)
                .Include(c => c.Member).FirstOrDefaultAsync(m => m.CurrentCallingID == id);

            if (CurrentCalling == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
