using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SacramentMeeting.Models;

namespace SacramentMeeting.Pages.Members
{
    public class DeleteModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public DeleteModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Member Member { get; set; }
        public string Message { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member = await _context.Member.FirstOrDefaultAsync(m => m.ID == id);

            if (Member == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member = await _context.Member
                .Include(m => m.Talks)
                .Include(m => m.Prayers)
                .Include(m => m.CurrentCallings)
                .FirstOrDefaultAsync(m => m.ID == id);
            
                if (Member != null)
                {
                try
                {
                    foreach (var talk in Member.Talks)
                    {
                        _context.Talk.Remove(talk);
                    }
                    foreach (var prayer in Member.Prayers)
                    {
                        _context.Prayer.Remove(prayer);
                    }
                    foreach (var calling in Member.CurrentCallings)
                    {
                        _context.CurrentCalling.Remove(calling);
                    }
                    _context.Member.Remove(Member);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");

                }
                catch (Exception)
                {
                    Message = "Error Deleting Member. Try again.";
                    return Page();
                }
                
            }
            Message = "Error Deleting Member. Try again.";
            return Page();
            
            
        }
    }
}
