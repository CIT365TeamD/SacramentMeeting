using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace SacramentMeeting.Models
{
    public enum GenderCl
    {
        Male, Female, Both
    }

    public enum Organizations
    {
        [Description("Bishopric")] Bishopric,
        [Description("Elder's Quorum")] Elders_Quorum,
        [Description("Relief Society")] Relief_Society,
        [Description("Young Men")] Young_Men,
        [Description("Young Women")] Young_Women,
        [Description("Primary")] Primary,
        [Description("Music")] Music
    }
    public class Calling
    {

        
        public int CallingID { get; set; }

        [Required, Display(Name = "Calling"),
            StringLength(50, MinimumLength = 2, ErrorMessage = "Title must be 2-50 characters."),
            RegularExpression(@"^[A-Z]+[a-zA-Z'\s-]*$")]
        public string Title { get; set; }

        [Required]
        public Organizations Organization { get; set; }

        [Display(Name = "Current Callings")]
        public ICollection<CurrentCalling> CurrentCallings { get; set; }

        public GenderCl CallingGender { get; set; }
    }
}
