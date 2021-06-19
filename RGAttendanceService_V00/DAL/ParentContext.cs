using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGAttendanceService_V00.Models;
using Microsoft.EntityFrameworkCore;

namespace RGAttendanceService_V00.DAL
{
    public class ParentContext : DbContext
    {
        public ParentContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Parent> Parent { get; set; }
        public DbSet<ParticipantParents> ParticipantParents { get; set; }
        //public DbSet<ParticipantGroups> ParticipantGroups { get; set; }
        public DbSet<Participant> Participant { get; set; }

        public DbSet<Coach> Coach { get; set; }

        public DbSet<Group> Group { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Many to many uczestnik/rodzic
            modelBuilder.Entity<ParticipantParents>()
                .HasKey(ke => new { ke.ParticipantId, ke.ParentId });

            modelBuilder.Entity<ParticipantParents>()
                .HasOne(b => b.Kid)
                .WithMany(bb => bb.Parents)
                .HasForeignKey(c => c.ParticipantId);

            modelBuilder.Entity<ParticipantParents>()
                .HasOne(p => p.Parent)
                .WithMany(pp => pp.Kids)
                .HasForeignKey(cc => cc.ParentId);

            //Many to many uczestnik/grupa

            //modelBuilder.Entity<ParticipantGroups>()
            //    .HasKey(k => new { k.ParticipantId, k.GroupId });

            //modelBuilder.Entity<ParticipantGroups>()
            //    .HasOne(p => p.Participant)
            //    .WithMany(g => g.Groups)
            //    .HasForeignKey(k => k.ParticipantId);

            //modelBuilder.Entity<ParticipantGroups>()
            //    .HasOne(g => g.Group)
            //    .WithMany(p => p.Participants)
            //    .HasForeignKey(k => k.GroupId);

            modelBuilder.Entity<Group>()
                .HasMany(p => p.Participants)
                .WithOne(g => g.Group)
                .HasForeignKey(f => f.GroupId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Coach>()
                .HasMany<Group>(g => g.Group)
                .WithOne(c => c.Coach)
                .OnDelete(DeleteBehavior.SetNull);

            

        }
        
        


    }
}
