using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Reservation
{
    public int Id { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "RoomId must be greater than 0")]
    public int RoomId { get; set; }
    
    [Required]
    [MinLength(1)]
    public string OrganizerName { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Topic { get; set; }
    
    public DateOnly Date { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
    
    [Required]
    [RegularExpression("planned|confirmed|cancelled", ErrorMessage = "Status must be: planned, confirmed or cancelled")]
    public string Status { get; set; }
}