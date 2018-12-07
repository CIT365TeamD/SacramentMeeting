using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SacramentMeeting.Models;

namespace SacramentMeeting.Pages.Callings
{
    public class CreateModel : CurrentCallingPageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public CreateModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }
        public List<Organizations> OrganizationsList;
        public IActionResult OnGet()
        {
            OrganizationsList = Enum.GetValues(typeof(Organizations)).Cast<Organizations>().ToList();

            return Page();
        }

        [BindProperty]
        public Calling Calling { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var emptyCalling = new Calling();

            if (await TryUpdateModelAsync<Calling>(
                emptyCalling,
                "calling",
                s => s.CallingID, s => s.Title, s => s.Organization))
            {
                _context.Calling.Add(emptyCalling);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            
             

            return Page();
        }
    }
}