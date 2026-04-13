using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    public static List<Reservation> Reservations = new List<Reservation>
    {
        new Reservation { Id = 1, RoomId = 1, OrganizerName = "Anna Kowalska", Topic = "Kolokwium z abc", Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12, 30), Status = "confirmed" },
        new Reservation { Id = 2, RoomId = 1, OrganizerName = "Jan Nowak", Topic = "APBD", Date = new DateOnly(2026, 5, 11), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(11, 0), Status = "planned" },
        new Reservation { Id = 3, RoomId = 2, OrganizerName = "Maria Wiśniewska", Topic = "AM", Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(15, 0), Status = "confirmed" },
        new Reservation { Id = 4, RoomId = 3, OrganizerName = "Piotr Zając", Topic = "ASD", Date = new DateOnly(2026, 5, 12), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(10, 0), Status = "planned" },
        new Reservation { Id = 5, RoomId = 4, OrganizerName = "Ewa Maj", Topic = "PPJ", Date = new DateOnly(2026, 5, 13), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(14, 0), Status = "cancelled" },
        new Reservation { Id = 6, RoomId = 2, OrganizerName = "Tomasz Król", Topic = "SDKP", Date = new DateOnly(2026, 5, 14), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12, 0), Status = "planned" },
    };

}