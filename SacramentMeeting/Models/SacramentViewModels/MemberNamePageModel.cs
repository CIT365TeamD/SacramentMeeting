using SacramentMeeting.Data;
using SacramentMeeting.Models;
using SacramentMeeting.Models.SacramentViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SacramentMeeting.Pages.Meetings
{
    public class MemberNamePageModel : PageModel
    {
        public SelectList MemberNamesSL { get; set; }

        public void PopulateMemberDropDownList(SacramentMeetingContext _context,
            object selectedMember = null)
        {
            var membersQuery = from m in _context.Member
                               orderby m.LastName
                               select m;

            MemberNamesSL = new SelectList(membersQuery.AsNoTracking(),
                "memberId", "FullName");
        }
    }
}
