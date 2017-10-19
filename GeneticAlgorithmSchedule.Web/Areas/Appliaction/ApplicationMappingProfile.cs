using AutoMapper;
using GeneticAlgorithmSchedule.Database.Models.Applications;
using GeneticAlgorithmSchedule.Web.Areas.Appliaction.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Areas.Appliaction
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<LoginViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, LoginViewModel>();
            CreateMap<RegisterViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, RegisterViewModel>();
        }
    }
}
