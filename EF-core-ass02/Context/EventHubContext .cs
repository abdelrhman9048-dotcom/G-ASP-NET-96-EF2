using EF_core_ass02.Configs;
using EF_core_ass02.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_core_ass02.Context
{
    public class EventHubContext : DbContext

    {
        public DbSet<Organizer> Organizers => Set<Organizer>();
        public DbSet<OrganizerProfile> OrganizerProfiles => Set<OrganizerProfile>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Attendee> Attendees => Set<Attendee>();
        public DbSet<Badge> Badges => Set<Badge>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                "Server=.;Database=EventHubDb;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Title).IsRequired().HasMaxLength(200);
                e.Property(x => x.Description).IsRequired().HasMaxLength(4000);
                e.Property(x => x.StartDate).IsRequired();
                e.Property(x => x.EndDate); // optional
                e.Property(x => x.MaxAttendees).IsRequired();

                e.HasOne(x => x.Organizer)
                 .WithMany(o => o.Events)
                 .HasForeignKey(x => x.OrganizerId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.ApplyConfiguration(new OrganizerProfileConfiguration());
            modelBuilder.ApplyConfiguration(new BadgeConfiguration());
           }
        } 
}
