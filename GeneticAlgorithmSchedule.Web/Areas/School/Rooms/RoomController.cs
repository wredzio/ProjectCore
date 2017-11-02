using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using GeneticAlgorithmSchedule.Database.Models.Schools;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Rooms
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomServicey;

        public RoomController(IRoomService roomService)
        {
            _roomServicey = roomService;
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var room = await _roomServicey.GetById(id);
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoomViewModel roomViewModel)
        {
            var room = await _roomServicey.Post(roomViewModel);
            return Ok(room);
        }
    }
}
