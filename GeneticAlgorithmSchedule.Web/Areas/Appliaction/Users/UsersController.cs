using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GeneticAlgorithmSchedule.Database.Models.Applications;
using Microsoft.AspNetCore.Identity;
using GeneticAlgorithmSchedule.Web.Controllers;
using Microsoft.Extensions.Logging;
using AutoMapper;
using GeneticAlgorithmSchedule.Web.Utils;
using GeneticAlgorithmSchedule.Web.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeneticAlgorithmSchedule.Web.Areas.Appliaction.Users
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class UsersController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IMapper _mapper;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<RegisterDto, ApplicationUser>(model);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Headmaster.ToString());
                    return Ok(model);
                }
                NotFound(result);
            }

            return BadRequest(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {          
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if(user == null)
                {
                    throw new UnauthorizedException("Bad Login Or Password", null, model);
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return Ok(model);
                }
                if (result.IsLockedOut)
                {
                    return BadRequest("Lockout");
                }
                else
                {
                    throw new UnauthorizedException("Bad Login Or Password", null, model);
                }
            }

            throw new BadRequestException("Bad Login Or Password", null, model);
        }
    }
}
