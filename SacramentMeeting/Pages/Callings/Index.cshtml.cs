using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SacramentMeeting.Models;

namespace SacramentMeeting.Pages.Callings
{
    public class IndexModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public IndexModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        public IList<Calling> Calling { get;set; }

        public async Task OnGetAsync()
        {
            Calling = await _context.Calling
                .Include(c => c.CurrentCallings)
                .ThenInclude(c => c.Member)
                .AsNoTracking()
                .OrderBy(c => c.Organization)
                .ToListAsync();
        }
    }
}
