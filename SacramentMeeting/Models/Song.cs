using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeeting.Models
{
    public class Song
    { 
    
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SongID { get; set; }

        [Required, Display(Name ="Song Title"),
            StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be 2-100 characters."),
            RegularExpression(@"^[A-Z]+[a-zA-Z',.\s-]*$")]
        public string Title { get; set; }

        public ICollection<SongSelection> SongSelections { get; set; }
    }
}
