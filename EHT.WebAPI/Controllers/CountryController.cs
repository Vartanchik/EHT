using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EHT.BLL.DTOs;
using EHT.BLL.Services.Concrete.CountryService;
using EHT.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHT.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountryController(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CountryDto), 200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Get(int id)
        {
            var country = await _countryService.GetByIdAsync(id);

            return country == null
                ? (ActionResult)NoContent()
                : Ok(country);
        }

        [HttpPost]
        //[Authorize(Policy = "RequireLoggedIn")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<ActionResult> Create([FromBody] CountryToCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel(400, "Invalid value was entered! Please, redisplay form."));
            }

            var countryDto = _mapper.Map<CountryDto>(model);

            var result = await _countryService.CreateOrUpdateAsync(countryDto);

            return result.Succeeded
                ? Ok(new ResponseModel(200, "Completed.", "Country created."))
                : (ActionResult)BadRequest(new ResponseModel(400, "Failed.", result.Error));
        }

        [HttpPut]
        //[Authorize(Policy = "RequireLoggedIn")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<ActionResult> Update([FromBody] CountryToUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel(400, "Invalid value was entered! Please, redisplay form."));
            }

            var countryDto = _mapper.Map<CountryDto>(model);

            var result = await _countryService.CreateOrUpdateAsync(countryDto);

            return result.Succeeded
                ? Ok(new ResponseModel(200, "Completed.", "Country updated."))
                : (ActionResult)BadRequest(new ResponseModel(400, "Failed.", result.Error));
        }

        [HttpDelete("{id}")]
        //[Authorize(Policy = "RequireLoggedIn")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _countryService.DeleteAsync(id);

            return result.Succeeded
                ? Ok(new ResponseModel(200, "Completed.", "Country deleted."))
                : (ActionResult)BadRequest(new ResponseModel(400, "Failed.", result.Error));
        }
    }
}