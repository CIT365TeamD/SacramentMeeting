using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacramentMeeting.Models
{
    public class Meeting
    {
        public int MeetingID { get; set; }

        [DataType(DataType.Date)]
        [Required, Display(Name = "Meeting Date")]
        public DateTime MeetingDate { get; set; }

        [Display(Name ="ConductorID")]
    
        public int CallingID { get; set; }
        
        [Display(Name ="Conducting")]
        public Calling Calling { get; set; }

        
        
        public ICollection<Prayer> Prayers { get; set; }
        public ICollection<Talk> Talks { get; set; }
        public ICollection<SongSelection> SongSelections { get; set; }

    }
}
