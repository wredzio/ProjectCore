using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Rooms
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private readonly IRoomsRepository _roomRepository;

        public RoomsController(IRoomsRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        // GET: api/Room
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Room/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var room = await _roomRepository.GetById(id);
            return Ok(room);
        }
        
        // POST: api/Room
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Room/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
