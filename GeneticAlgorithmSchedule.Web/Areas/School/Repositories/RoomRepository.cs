using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneticAlgorithmSchedule.Models;
using GeneticAlgorithmSchedule.Web.Areas.School.Contexts;
using GeneticAlgorithmSchedule.Web.Areas.School.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private SchoolContext _schoolContext;

        public RoomRepository(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public Room GetById(int id)
        {
            return _schoolContext.Rooms.FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Room> GetByQuery()
        {
            return _schoolContext.Rooms;
        }

        public Room Post(Room item)
        {
            _schoolContext.Rooms.Add(item);
            _schoolContext.SaveChanges();
            return item;
        }

        public Room Put(Room item)
        {
            Room room = GetById(item.Id);
            room = item;
            _schoolContext.SaveChanges();
            return room;
        }

        public void Delete(int id)
        {
            _schoolContext.Rooms.Remove(GetById(id));
            _schoolContext.SaveChanges();
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _schoolContext.Rooms.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Room>> GetByQueryAsync()
        {
            return await _schoolContext.Rooms.ToListAsync();
        }

        public async Task<Room> PostAsync(Room item)
        {
            _schoolContext.Rooms.Add(item);
            await _schoolContext.SaveChangesAsync();
            return item;
        }

        public async Task<Room> PutAsync(Room item)
        {
            Room room = GetById(item.Id);
            room = item;
            await _schoolContext.SaveChangesAsync();
            return room;
        }

        public async Task DeleteAsync(int id)
        {
            _schoolContext.Rooms.Remove(GetById(id));
            await _schoolContext.SaveChangesAsync();
        }
    }
}
