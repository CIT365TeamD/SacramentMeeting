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
        public DateTime CurrentStart { get; set; }
        public DateTime CurrentEnd { get; set; }

        public async Task OnGetAsync(DateTime? startDate, DateTime? endDate)
        {
            //if (startDate.HasValue)
            //{
            //    CurrentStart = startDate.Value;
            //}
            //if (endDate.HasValue)
            //{
                
            //    CurrentEnd = endDate.Value;
            //    if (CurrentEnd < CurrentStart)
            //    {
            //        CurrentEnd = CurrentStart.AddYears(1);
            //    }
            //}
            
            IQueryable<Meeting> meetings = from m in _context.Meeting
                                           select m;

            // if both startDate and EndDate supplied
            if (startDate.HasValue && endDate.HasValue)
            {
                
                CurrentStart = startDate.Value;
                CurrentEnd = endDate.Value;
                // if end date is before start date, change end date to one year after start date;
                if (CurrentStart > CurrentEnd)
                {
                    CurrentEnd = CurrentStart.AddYears(1);
                }
                 
                meetings = meetings.Where(m => m.MeetingDate >= CurrentStart && m.MeetingDate <= CurrentEnd);
            }
            else if (startDate.HasValue)
            {
                CurrentStart = startDate.Value;
                

                meetings = meetings.Where(m => m.MeetingDate >= CurrentStart);
            }
            Meeting = await meetings
                .Include(m => m.Calling)
                    .ThenInclude(m => m.CurrentCallings)
                        .ThenInclude(m => m.Member)
                        .OrderBy(m => m.MeetingDate)
                .ToListAsync();
        }
    }
}
