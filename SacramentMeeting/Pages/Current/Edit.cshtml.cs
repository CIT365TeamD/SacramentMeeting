﻿using System;
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
    public class EditModel : PageModel
    {
        private readonly SacramentMeeting.Models.SacramentMeetingContext _context;

        public EditModel(SacramentMeeting.Models.SacramentMeetingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CurrentCalling CurrentCalling { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CurrentCalling = await _context.CurrentCalling
                .Include(c => c.Calling)
                .Include(c => c.Member).FirstOrDefaultAsync(m => m.CurrentCallingID == id);

            if (CurrentCalling == null)
            {
                return NotFound();
            }
           ViewData["CallingID"] = new SelectList(_context.Calling, "CallingID", "Display");
           ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
            return Page();
        }

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
                ViewData["CallingID"] = new SelectList(_context.Calling, "CallingID", "Display");
                ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
                return Page();
            }

            _context.Attach(CurrentCalling).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrentCallingExists(CurrentCalling.CurrentCallingID))
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

        private bool CurrentCallingExists(int id)
        {
            return _context.CurrentCalling.Any(e => e.CurrentCallingID == id);
        }
    }
}
