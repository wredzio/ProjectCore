using GeneticAlgorithmSchedule.Database.Models.Applications;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Areas.Appliaction.Users
{
    public interface IUserService
    {
        Task<IdentityResult> Register(RegisterDto registerDto, ApplicationUser applicationUser);
        Task<SignInResult> Login(LoginDto loginDto);

    }
}
