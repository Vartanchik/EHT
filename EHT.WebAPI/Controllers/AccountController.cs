
using AutoMapper;
using EHT.BLL.DTOs;
using EHT.BLL.Services.Concrete.AppUserService;
using EHT.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EHT.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountController(IAppUserService appUserService, IMapper mapper, IConfiguration configuration)
        {
            _appUserService = appUserService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel(400, "Invalid value was entered! Please, redisplay form."));
            }

            var appUserDto = _mapper.Map<AppUserDto>(model);

            var result = await _appUserService.CreateAsync(appUserDto, model.Password);

            return result.Succeeded
                ? Ok(new ResponseModel(200, "Completed.", "User created."))
                : (ActionResult)BadRequest(new ResponseModel(400, "Failed.", result.Error));
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel(400, "Invalid value was entered! Please, redisplay form."));
            }

            var userId = await _appUserService.GetUserIdAsync(model.Email);

            if (userId < 1)
            {
                return BadRequest(new ResponseModel(400, "Failed", "User not found."));
            }

            var passwordIsRight = await _appUserService.CheckPasswordAsync(userId, model.Password);

            if (!passwordIsRight)
            {
                return BadRequest(new ResponseModel(400, "Failed", "Wrong password."));
            }

            var token = _appUserService.GenerateToken(userId, _configuration["Jwt:Key"], _configuration["Jwt:ExpireTime"]);

            return Ok(new { Token = token });
        }

    }
}