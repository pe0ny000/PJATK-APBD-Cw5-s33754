using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    public static List<Reservation> reservations = new List<Reservation>
    {
        new Reservation
        {
            Id = 1, RoomId = 1, OrganizerName = "Anna Kowalska", Topic = "Kolokwium z abc",
            Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12, 30),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2, RoomId = 1, OrganizerName = "Jan Nowak", Topic = "APBD", Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(11, 0), Status = "planned"
        },
        new Reservation
        {
            Id = 3, RoomId = 2, OrganizerName = "Maria Wiśniewska", Topic = "AM", Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(15, 0), Status = "confirmed"
        },
        new Reservation
        {
            Id = 4, RoomId = 3, OrganizerName = "Piotr Zając", Topic = "ASD", Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(10, 0), Status = "planned"
        },
        new Reservation
        {
            Id = 5, RoomId = 4, OrganizerName = "Ewa Maj", Topic = "PPJ", Date = new DateOnly(2026, 5, 13),
            StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(14, 0), Status = "cancelled"
        },
        new Reservation
        {
            Id = 6, RoomId = 2, OrganizerName = "Tomasz Król", Topic = "SDKP", Date = new DateOnly(2026, 5, 14),
            StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12, 0), Status = "planned"
        },
    };

    /*[HttpGet]
    public IActionResult GetAllReservations()
    {
        return Ok(reservations);
    }*/

    [HttpGet("/reservations/{id}")]
    public IActionResult GetReservationById(int id)
    {
        return  Ok(reservations.FirstOrDefault(r => r.Id == id));
    }

    [HttpGet]
    [Route("/reservations")]
    public IActionResult GetAll(DateOnly? date, string? status, int? roomId)
    {
        var filteredReservations = ReservationsController.reservations.AsQueryable();

        if (date.HasValue)
            filteredReservations = filteredReservations.Where(r => r.Date == date.Value);

        if (!string.IsNullOrEmpty(status))
            filteredReservations = filteredReservations.Where(r => r.Status == status);

        if (roomId.HasValue)
            filteredReservations = filteredReservations.Where(r => r.RoomId == roomId.Value);

        return Ok(filteredReservations.ToList());
    }


    [HttpPut]
    [Route("/reservations/{id}")]
    public IActionResult Update(int id, Reservation reservation)
    {
        if (reservation.EndTime <= reservation.StartTime)
            return BadRequest("EndTime must be later than StartTime");

        var existing = ReservationsController.reservations.FirstOrDefault(r => r.Id == id);
    
        if (existing == null)
            return NotFound($"Reservation with id {id} not found");

        var room = ReservationsController.reservations.FirstOrDefault(r => r.Id == reservation.RoomId);
    
        if (room == null)
            return NotFound($"Room with id {reservation.RoomId} not found");

        bool hasConflict = ReservationsController.reservations.Any(r =>
            r.Id != id &&
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            r.StartTime < reservation.EndTime &&
            r.EndTime > reservation.StartTime);

        if (hasConflict)
            return Conflict("Reservation conflicts with another reservation in this room");

        existing.RoomId = reservation.RoomId;
        existing.OrganizerName = reservation.OrganizerName;
        existing.Topic = reservation.Topic;
        existing.Date = reservation.Date;
        existing.StartTime = reservation.StartTime;
        existing.EndTime = reservation.EndTime;
        existing.Status = reservation.Status;

        return Ok(existing);
    }
    
    [HttpDelete]
    [Route("/reservations/{id}")]
    public IActionResult Delete(int id)
    {
        var reservation = ReservationsController.reservations.FirstOrDefault(r => r.Id == id);
    
        if (reservation == null)
            return NotFound($"Reservation with id {id} not found");

        ReservationsController.reservations.Remove(reservation);
    
        return NoContent();
    }
}