using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using GeneticAlgorithmSchedule.Database.Models;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Rooms
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private readonly IRoomsRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomsController(IRoomsRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var room = await _roomRepository.GetById(id);
            return Ok(_mapper.Map<RoomViewModel>(room));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoomViewModel roomViewModel)
        {
            var a = _mapper.Map<Room>(roomViewModel);

            var room = await _roomRepository.Post(a);
            return Ok(_mapper.Map<RoomViewModel>(room));
        }
    }
}
