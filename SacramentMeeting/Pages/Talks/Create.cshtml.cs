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
    public class CreateModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public CreateModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }
        public Meeting Meeting { get; set; }
        public bool Edit { get; set; }
        public async Task<IActionResult> OnGetAsync(int id, bool? edit)
        {

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

            ViewData["MeetingID"] = new SelectList(_context.Meeting, "MeetingID", "MeetingID");
            ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
            return Page();
        }

        [BindProperty]
        public Talk Talk { get; set; }

        public async Task<IActionResult> OnPostAsync(bool? edit)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var newTalk = new Talk();
            if (await TryUpdateModelAsync<Talk>(
                newTalk,
                "talk",
                i => i.MeetingID, i => i.MemberID, i => i.Topic))
            {
                _context.Talk.Add(newTalk);
                await _context.SaveChangesAsync();

                if (Edit)
                {
                    return RedirectToPage("../Meetings/Edit", new { id = newTalk.MeetingID });
                }
                return RedirectToPage("./Create", new {id = newTalk.MeetingID });
            }

            if (Edit)
            {
                return RedirectToPage("../Meetings/Edit", new { id = newTalk.MeetingID });
            }
            return RedirectToPage("./Create", new { id = newTalk.MeetingID });
        }
    }
}