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
    public class IndexModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public IndexModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        public IList<Meeting> Meeting { get;set; }
        public IList<Member> Member { get; set; }

        public async Task OnGetAsync()
        {
            Meeting = await _context.Meeting
                .Include(m => m.Calling)
                    .ThenInclude(m => m.CurrentCallings)
                        .ThenInclude(m => m.Member)
                           .OrderBy(m => m.MeetingDate)
                .ToListAsync();
        }
    }
}
