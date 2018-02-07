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
using GeneticAlgorithmSchedule.Web.Emails.PostBoxs;
using GeneticAlgorithmSchedule.Web.Emails.EmailBuilders.ConfirmAccount;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeneticAlgorithmSchedule.Web.Areas.Appliaction.Users
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<RegisterDto, ApplicationUser>(model);

                var result = await _userService.Register(model, user);

                if (result.Succeeded)
                {
                    return Ok(model);
                }

                return NotFound(result);
            }

            return BadRequest(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Login(model);
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
