using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacramentMeeting.Models
{
    public class Talk
    {
        public int TalkID { get; set; }
        public int MeetingID { get; set; }
        public int MemberID { get; set; }

        [StringLength(250, MinimumLength = 2, ErrorMessage = "Topic must be 2-250 characters."),
            RegularExpression(@"[a-zA-Z""'.,\s-]*$")]
        public string Topic { get; set; }

        public Meeting Meeting { get; set; }
        public Member Member { get; set; }
    }
}
