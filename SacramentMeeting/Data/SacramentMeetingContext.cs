using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SacramentMeeting.Models
{
    public class SacramentMeetingContext : DbContext
    {
        public SacramentMeetingContext (DbContextOptions<SacramentMeetingContext> options)
            : base(options)
        {
        }

        public DbSet<SacramentMeeting.Models.Member> Member { get; set; }
        public DbSet<SacramentMeeting.Models.Calling> Calling { get; set; }
        public DbSet<SacramentMeeting.Models.Meeting> Meeting { get; set; }
        public DbSet<SacramentMeeting.Models.Prayer> Prayer { get; set; }
        public DbSet<SacramentMeeting.Models.Song> Song { get; set; }
        public DbSet<SacramentMeeting.Models.Talk> Talk { get; set; }
        public DbSet<SacramentMeeting.Models.CurrentCalling> CurrentCalling { get; set; }
        public DbSet<SacramentMeeting.Models.SongSelection> SongSelection { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Meeting>()
                .HasIndex(m => m.MeetingDate)
                .IsUnique();

            builder.Entity<Member>()
                .HasIndex(m => new { m.FirstName, m.LastName })
                .IsUnique();

            
        }
    }
}
