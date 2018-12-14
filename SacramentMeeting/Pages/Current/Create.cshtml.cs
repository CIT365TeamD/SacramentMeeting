using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SacramentMeeting.Models;

namespace SacramentMeeting.Pages.Current
{
    public class CreateModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public CreateModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CallingID"] = new SelectList(_context.Calling, "CallingID", "Title");
        ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
            return Page();
        }

        [BindProperty]
        public CurrentCalling CurrentCalling { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CurrentCalling.Add(CurrentCalling);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}