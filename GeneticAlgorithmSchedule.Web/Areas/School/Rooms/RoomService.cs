using AutoMapper;
using GeneticAlgorithmSchedule.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Rooms
{
    public class RoomService : IRoomService
    {
        private IRoomRepository _roomsRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomsRepository, IMapper mapper)
        {
            _roomsRepository = roomsRepository;
            _mapper = mapper;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RoomViewModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoomViewModel>> GetByQuery()
        {
            throw new NotImplementedException();
        }

        public Task<RoomViewModel> Post(RoomViewModel item)
        {
            throw new NotImplementedException();
        }

        public Task<RoomViewModel> Put(RoomViewModel item)
        {
            throw new NotImplementedException();
        }
    }
}
