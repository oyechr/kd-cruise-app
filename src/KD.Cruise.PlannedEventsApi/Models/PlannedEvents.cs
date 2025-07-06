namespace PlannedEventsApi.Models; 

public class PlannedEvent
{
    public Guid Id { get; set; }
    public int VoyageId { get; set; } 
    public int VesselId { get; set; } 
    public DateTime FromUtc { get; set; }
    public DateTime ToUtc { get; set; }
    public string Event { get; set; } = null!;

}