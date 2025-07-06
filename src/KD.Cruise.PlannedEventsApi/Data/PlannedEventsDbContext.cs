using Microsoft.EntityFrameworkCore;
using PlannedEventsApi.Models;

namespace PlannedEventsApi.Data;

public class PlannedEventsDbContext : DbContext
{
    public PlannedEventsDbContext(DbContextOptions<PlannedEventsDbContext> options)
     : base(options)
    {
    }
    public DbSet<PlannedEvent> PlannedEvents { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlannedEvent>().ToTable("planned_events_tab");

        modelBuilder.Entity<PlannedEvent>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<PlannedEvent>()
            .Property(e => e.Id)
            .HasColumnName("id");

        modelBuilder.Entity<PlannedEvent>()
            .Property(e => e.VoyageId)
            .HasColumnName("voyage_id")
            .IsRequired();

        modelBuilder.Entity<PlannedEvent>()
            .Property(e => e.VesselId)
            .HasColumnName("vessel_id")
            .IsRequired();

        modelBuilder.Entity<PlannedEvent>()
            .Property(e => e.FromUtc)
            .HasColumnName("from_date")
            .IsRequired();

        modelBuilder.Entity<PlannedEvent>()
            .Property(e => e.ToUtc)
            .HasColumnName("to_date")
            .IsRequired();

        modelBuilder.Entity<PlannedEvent>()
            .Property(e => e.Event)
            .HasColumnName("event")
            .IsRequired();
    }

}