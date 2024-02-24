using LuftBornTest.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using LuftBornTest.Core.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using LuftBornTest.Infrastructure.Services;
using Mango.Services.AuthAPI.Models.Dto;

namespace LuftBornTest.Web.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _service;

        public UserController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegistrationRequest model)
        {
            var user = new AppUser
            {
                UserName = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            try
            {
                if (!result.Succeeded)
                {
                    return BadRequest();
                }

                var userReturn = new LoginResponse()
                {
                    Token = await _service.CreateToken(user, _userManager)
                };
                return Ok(userReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Core.Entities.LoginRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            var userReturn = new LoginResponse()
            {
                Token = await _service.CreateToken(user, _userManager)
            };
            return Ok(userReturn);
        }
    }
}