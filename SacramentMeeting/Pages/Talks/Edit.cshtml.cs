using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentMeeting.Models;

namespace SacramentMeeting.Pages.Talks
{
    public class EditModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public EditModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Talk Talk { get; set; }
        public Meeting Meeting { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Talk = await _context.Talk
                .Include(t => t.Meeting)
                .Include(t => t.Member).FirstOrDefaultAsync(m => m.TalkID == id);

            if (Talk == null)
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
                           .FirstOrDefaultAsync(m => m.MeetingID == Talk.MeetingID);

            
           ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var talkToUpdate = await _context.Talk.FindAsync(id);

            if (await TryUpdateModelAsync<Talk>(
                talkToUpdate,
                "talk",
                t => t.MemberID, t => t.TalkID, t => t.Topic))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Details", new { id = Talk.MeetingID });
            }

            ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
            return Page();
        }
    }
}
