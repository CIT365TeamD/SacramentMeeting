using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SacramentMeeting.Models
{
    public enum GenderCl
    {
        Male, Female, Both
    }

    public enum Organizations
    {
        [Display(Name ="Bishopric")] Bishopric,
        [Display(Name ="Elder's Quorum")] Elders_Quorum,
        [Display(Name = "Relief Society")] Relief_Society,
        [Display(Name = "Young Men")] Young_Men,
        [Display(Name = "Young Women")] Young_Women,
        [Display(Name = "Primary")] Primary,
        [Display(Name = "Music")] Music
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

        public string Display
        {
            get
            {
                string[] org = Organization.ToString().Split('_');
                string organization = string.Join(" ", org);
                return Title + " - " + organization;
            }
                }
        
    }
}
