using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacramentMeeting.Models
{
    public class Member
    {
        public int ID { get; set; }

        [Required, Display(Name ="Last Name"), 
            StringLength(50, MinimumLength =2, ErrorMessage ="Last Name must be 2-50 characters."),
            RegularExpression(@"^[A-Z]+[a-zA-Z'\s-]*$")]
        public string LastName { get; set; }

        [Required, Display(Name = "First Name"),
            StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be 2-50 characters."),
            RegularExpression(@"^[A-Z]+[a-zA-Z'.,\s-]*$")]
        public string FirstName { get; set; }

        [Display(Name ="Member Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public ICollection<CurrentCalling> CurrentCallings { get; set; }
        public ICollection<Talk> Talks { get; set; }
        public ICollection<Prayer> Prayers { get; set; }
        
    }
}
