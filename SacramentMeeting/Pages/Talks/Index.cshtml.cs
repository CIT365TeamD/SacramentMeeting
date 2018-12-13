using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SacramentMeeting.Models;

namespace SacramentMeeting.Pages.Talks
{
    public class IndexModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public IndexModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        public IList<Talk> Talk { get;set; }

        public async Task OnGetAsync()
        {
            Talk = await _context.Talk
                .Include(t => t.Meeting)
                .Include(t => t.Member).ToListAsync();
        }
    }
}
