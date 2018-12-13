﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SacramentMeeting.Models;

namespace SacramentMeeting.Migrations
{
    [DbContext(typeof(SacramentMeetingContext))]
    [Migration("20181211232143_ComplexModel4")]
    partial class ComplexModel4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SacramentMeeting.Models.Calling", b =>
                {
                    b.Property<int>("CallingID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Organization");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("CallingID");

                    b.ToTable("Calling");
                });

            modelBuilder.Entity("SacramentMeeting.Models.CurrentCalling", b =>
                {
                    b.Property<int>("CurrentCallingID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CallingID");

                    b.Property<DateTime>("DateCalled");

                    b.Property<int>("MemberID");

                    b.HasKey("CurrentCallingID");

                    b.HasIndex("CallingID");

                    b.HasIndex("MemberID");

                    b.ToTable("CurrentCalling");
                });

            modelBuilder.Entity("SacramentMeeting.Models.Meeting", b =>
                {
                    b.Property<int>("MeetingID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CallingID");

                    b.Property<DateTime>("MeetingDate");

                    b.HasKey("MeetingID");

                    b.HasIndex("CallingID");

                    b.HasIndex("MeetingDate")
                        .IsUnique();

                    b.ToTable("Meeting");
                });

            modelBuilder.Entity("SacramentMeeting.Models.Member", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("FirstName", "LastName")
                        .IsUnique();

                    b.ToTable("Member");
                });

            modelBuilder.Entity("SacramentMeeting.Models.Prayer", b =>
                {
                    b.Property<int>("PrayerID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MeetingID");

                    b.Property<int>("MemberID");

                    b.Property<int>("Schedule");

                    b.HasKey("PrayerID");

                    b.HasIndex("MemberID");

                    b.HasIndex("MeetingID", "Schedule")
                        .IsUnique();

                    b.ToTable("Prayer");
                });

            modelBuilder.Entity("SacramentMeeting.Models.Song", b =>
                {
                    b.Property<int>("SongID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("SongID");

                    b.ToTable("Song");
                });

            modelBuilder.Entity("SacramentMeeting.Models.SongSelection", b =>
                {
                    b.Property<int>("SongSelectionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MeetingID");

                    b.Property<int>("Schedule");

                    b.Property<int>("SongID");

                    b.HasKey("SongSelectionID");

                    b.HasIndex("SongID");

                    b.HasIndex("MeetingID", "Schedule")
                        .IsUnique();

                    b.ToTable("SongSelection");
                });

            modelBuilder.Entity("SacramentMeeting.Models.Talk", b =>
                {
                    b.Property<int>("TalkID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MeetingID");

                    b.Property<int>("MemberID");

                    b.Property<string>("Topic")
                        .HasMaxLength(250);

                    b.HasKey("TalkID");

                    b.HasIndex("MeetingID");

                    b.HasIndex("MemberID");

                    b.ToTable("Talk");
                });

            modelBuilder.Entity("SacramentMeeting.Models.CurrentCalling", b =>
                {
                    b.HasOne("SacramentMeeting.Models.Calling", "Calling")
                        .WithMany("CurrentCallings")
                        .HasForeignKey("CallingID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SacramentMeeting.Models.Member", "Member")
                        .WithMany("CurrentCallings")
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SacramentMeeting.Models.Meeting", b =>
                {
                    b.HasOne("SacramentMeeting.Models.Calling", "Calling")
                        .WithMany()
                        .HasForeignKey("CallingID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SacramentMeeting.Models.Prayer", b =>
                {
                    b.HasOne("SacramentMeeting.Models.Meeting", "Meeting")
                        .WithMany("Prayers")
                        .HasForeignKey("MeetingID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SacramentMeeting.Models.Member", "Member")
                        .WithMany("Prayers")
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SacramentMeeting.Models.SongSelection", b =>
                {
                    b.HasOne("SacramentMeeting.Models.Meeting", "Meeting")
                        .WithMany("SongSelections")
                        .HasForeignKey("MeetingID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SacramentMeeting.Models.Song", "Song")
                        .WithMany("SongSelections")
                        .HasForeignKey("SongID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SacramentMeeting.Models.Talk", b =>
                {
                    b.HasOne("SacramentMeeting.Models.Meeting", "Meeting")
                        .WithMany("Talks")
                        .HasForeignKey("MeetingID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SacramentMeeting.Models.Member", "Member")
                        .WithMany("Talks")
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
