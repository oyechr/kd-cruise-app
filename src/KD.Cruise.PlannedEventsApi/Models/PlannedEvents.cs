namespace PlannedEventsApi.Models; 

public class PlannedEvent
{
    public Guid Id { get; set; }
    public string VoyageId { get; set; } = null!;
    public string VesselId { get; set; } = null!;
    public DateTime FromUtc { get; set; }
    public DateTime ToUtc { get; set; }
    public string Event { get; set; } = null!;

}