
using AutoMapper;
using EHT.BLL.DTOs;
using EHT.BLL.Services.Concrete.AppUserService;
using EHT.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EHT.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;

        public AccountController(IAppUserService appUserService, IMapper mapper)
        {
            _appUserService = appUserService;
            _mapper = mapper;
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login(string returnUrl = null)
        //{
        //    return null;
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        //{
        //    return null;
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Logout()
        //{
        //    return null;
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public IActionResult Register(string returnUrl = null)
        //{
        //    return null;
        //}

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
    }
}