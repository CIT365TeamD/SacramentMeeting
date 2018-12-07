using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacramentMeeting.Models
{
    public enum PrayerPosition
    {
        Opening, Closing
    }
    public class Prayer
    {
        public int PrayerID { get; set; }
        public int MemberID { get; set; }
        public int MeetingID { get; set; }

        [Required]
        public PrayerPosition Schedule { get; set; }

        public Member Member { get; set; }
        public Meeting Meeting { get; set; }
    }
}
