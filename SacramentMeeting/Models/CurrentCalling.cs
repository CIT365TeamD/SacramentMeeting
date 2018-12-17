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

        [Display(Name = "Calling")]
        public int CallingID { get; set; }

        [Display(Name = "Member Name")]
        public int MemberID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Called")]
        public DateTime DateCalled { get; set; }

        public Calling Calling { get; set; }

        [Display(Name = "Member Name")]
        public Member Member { get; set; }

        public GenderCl CallingGender { get; set; }



    }
}
