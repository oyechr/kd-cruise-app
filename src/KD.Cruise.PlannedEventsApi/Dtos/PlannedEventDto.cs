using System.ComponentModel.DataAnnotations;

namespace PlannedEventsApi.Dtos; 

public class PlannedEventDto
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public string VoyageId { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string VesselId { get; set; } = null!;

    [Required]
    public DateTime FromUtc { get; set; }

    [Required]
    public DateTime ToUtc { get; set; }

    [Required]
    [StringLength(100)]
    public string Event { get; set; } = null!;
}