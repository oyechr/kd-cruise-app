using System.ComponentModel.DataAnnotations;

namespace PlannedEventsApi.Dtos; 

public class PlannedEventDto
{
    public Guid Id { get; set; }

    [Required]
    public int VoyageId { get; set; } 

    [Required]
    public int VesselId { get; set; }

    [Required]
    public DateTime FromUtc { get; set; }

    [Required]
    public DateTime ToUtc { get; set; }

    [Required]
    [StringLength(100)]
    public string Event { get; set; } = null!;
}