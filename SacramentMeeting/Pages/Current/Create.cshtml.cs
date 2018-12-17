using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public Calling Calling { get; set; }
        public string Message { get; set; }
        public Member Member { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Calling = await _context.Calling.FirstOrDefaultAsync(m => m.CallingID == CurrentCalling.CallingID);
            Member = await _context.Member.FirstOrDefaultAsync(m => m.ID == CurrentCalling.MemberID);

            if (Calling.CallingGender != GenderCl.Both && Calling.CallingGender.ToString() != Member.MembersGender.ToString())
            {
                Message = "Member is wrong gender for this calling.";
                ViewData["CallingID"] = new SelectList(_context.Calling, "CallingID", "Title");
                ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
                return Page();
            }

            _context.CurrentCalling.Add(CurrentCalling);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}