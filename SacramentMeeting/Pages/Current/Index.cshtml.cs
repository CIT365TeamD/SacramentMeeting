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
    public class IndexModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public IndexModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        public IList<CurrentCalling> CurrentCalling { get;set; }

        public async Task OnGetAsync()
        {
            CurrentCalling = await _context.CurrentCalling
                .Include(c => c.Calling)
                .Include(c => c.Member)
                .ToListAsync();
        }
    }

}
