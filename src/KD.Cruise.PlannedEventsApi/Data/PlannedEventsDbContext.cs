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
            .Property(e => e.VoyageId)
            .IsRequired();
        modelBuilder.Entity<PlannedEvent>()
            .Property(e => e.VesselId)
            .IsRequired();
        modelBuilder.Entity<PlannedEvent>()
            .Property(e => e.Event)
            .IsRequired();
    }
}