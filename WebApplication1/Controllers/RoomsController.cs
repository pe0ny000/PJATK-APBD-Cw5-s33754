using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsControler : ControllerBase
{
    public static List<Room> rooms =
    [
        new Room
        {
            Id = 1, Name = "A 101", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true
        },
        new Room
        {
            Id = 2, Name = "B 204", BuildingCode = "B", Floor = 2, Capacity = 24, HasProjector = true, IsActive = true
        },
        new Room
        {
            Id = 3, Name = "A 301", BuildingCode = "A", Floor = 3, Capacity = 15, HasProjector = false,
            IsActive = true
        },
        new Room
        {
            Id = 4, Name = "Aula B1", BuildingCode = "B", Floor = 0, Capacity = 100, HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 5, Name = "A 102", BuildingCode = "A", Floor = 1, Capacity = 30, HasProjector = false,
            IsActive = false
        }
    ];

    [HttpGet]
    public IActionResult GetAllRooms()
    {
        return Ok(rooms.Select(e => new RoomDto() { Id = e.Id, Name = e.Name }));
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetRoomById(int id)
    {
        var room = rooms.FirstOrDefault(e => e.Id == id);
        if (room == null)
            return NotFound($"Room with id {id} not found");

        return Ok(new RoomDto()
        {
            Id = room.Id,
            Name = room.Name,
            Capacity = room.Capacity,
            HasProjector = room.HasProjector,
            IsActive = room.IsActive,
            BuildingCode = room.BuildingCode,
            Floor = room.Floor
        });
    }


    [HttpGet("building/{buildingCode}")]
    public IActionResult GetRoomByBuildingCode(string buildingCode)
    {
        var roomsInBuilding = RoomsControler.rooms.Where(r => r.BuildingCode == buildingCode).ToList();

        if (roomsInBuilding.Count == 0)
            return NotFound($"No rooms found in building {buildingCode}");
        return Ok(new RoomDto());
    }


    [HttpGet]
    public IActionResult GetRoomsFiltered(int? minCapacity, bool? hasProjector, bool? isActive)
    {
        var filteredRooms = RoomsControler.rooms.AsQueryable();

        if (minCapacity.HasValue)
            filteredRooms = filteredRooms.Where(r => r.Capacity >= minCapacity.Value);
        if (hasProjector.HasValue)
            filteredRooms = filteredRooms.Where(r => r.HasProjector == hasProjector.Value);
        if (isActive.HasValue)
            filteredRooms = filteredRooms.Where(r => r.IsActive);
        return Ok(filteredRooms.ToList());
    }

    [HttpPost]
    public IActionResult CreateRoom([FromBody] RoomDto room)
    {
        var newRoom = new Room()
        {
            Id = rooms.Max(e => e.Id) + 1,
            Name = room.Name,
            Capacity = room.Capacity,
            BuildingCode = room.BuildingCode,
            Floor = room.Floor,
            IsActive = room.IsActive,
            HasProjector = room.HasProjector
        };
        rooms.Add(newRoom);
        return CreatedAtAction(nameof(GetRoomById), new { id = newRoom.Id }, new RoomDto());
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Room room)
    {
        var existing = RoomsControler.rooms.FirstOrDefault(r => r.Id == id);

        if (existing == null)
            return NotFound($"Room with id {id} not found");

        existing.Name = room.Name;
        existing.BuildingCode = room.BuildingCode;
        existing.Floor = room.Floor;
        existing.Capacity = room.Capacity;
        existing.HasProjector = room.HasProjector;
        existing.IsActive = room.IsActive;

        return Ok(existing);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete(int id)
    {
        var room = rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
            return NotFound($"Room at id {id} not found");

        rooms.Remove(room);
        return NoContent();
    }
}