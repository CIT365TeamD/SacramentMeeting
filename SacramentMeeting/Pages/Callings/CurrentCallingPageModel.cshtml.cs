using SacramentMeeting.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SacramentMeeting.Models;

namespace SacramentMeeting.Pages.Callings
{
    public class CurrentCallingPageModel : PageModel
    {
        public SelectList MemberNameSL { get; set; }

        public void PopulateMembersDropDownList(SacramentMeetingContext _context,
            object selectedMember = null)
        {
            var memberQuery = from m in _context.Member
                              orderby m.LastName
                              select m;

            MemberNameSL = new SelectList(memberQuery.AsNoTracking(),
                "MemberID", "FullName", selectedMember);
        }
    }
}
