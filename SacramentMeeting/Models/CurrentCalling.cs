using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeeting.Models
{
    public class CurrentCalling
    {
        public int CurrentCallingID { get; set; }
        public int CallingID { get; set; }
        public int MemberID { get; set; }
        [Display(Name = "Date Called")]
        public DateTime DateCalled { get; set; }

        public Calling Calling { get; set; }
        public Member Member { get; set; }



    }
}
