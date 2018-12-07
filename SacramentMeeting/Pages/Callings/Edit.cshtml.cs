using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentMeeting.Models;


namespace SacramentMeeting.Pages.Callings
{
    public class EditModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public EditModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Calling Calling { get; set; }
        public List<Organizations> OrganizationsList;
        public async Task<IActionResult> OnGetAsync(int? id)
        {

            OrganizationsList = Enum.GetValues(typeof(Organizations)).Cast<Organizations>().ToList();
            if (id == null)
            {
                return NotFound();
            }

            Calling = await _context.Calling.FirstOrDefaultAsync(m => m.CallingID == id);
            
            if (Calling == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Calling).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CallingExists(Calling.CallingID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CallingExists(int id)
        {
            return _context.Calling.Any(e => e.CallingID == id);
        }
    }
}
