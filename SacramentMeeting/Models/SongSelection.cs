using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacramentMeeting.Models
{
    public enum SongPosition
    {
        Opening, Sacrament, Closing
    }
    public class SongSelection
    {
        public int SongSelectionID { get; set; }
        public int SongID { get; set; }
        public int MeetingID { get; set; }

        [Required]
        public SongPosition Schedule { get; set; }

        public Song Song { get; set; }
        public Meeting Meeting { get; set; }
    }
}
