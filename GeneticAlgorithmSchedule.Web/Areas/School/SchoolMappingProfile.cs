using AutoMapper;
using GeneticAlgorithmSchedule.Database.Models.Applications;
using GeneticAlgorithmSchedule.Database.Models.Schools;
using GeneticAlgorithmSchedule.Web.Areas.Appliaction.Users;
using GeneticAlgorithmSchedule.Web.Areas.School.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Areas.School
{
    public class SchoolMappingProfile : Profile
    {
        public SchoolMappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Room, RoomViewModel>();
            CreateMap<RoomViewModel, Room>();
            CreateMap<LoginDto, ApplicationUser>();
            CreateMap<ApplicationUser, LoginDto>();
        }
    }
}
