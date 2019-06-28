using System.Threading.Tasks;
using AutoMapper;
using EHT.BLL.DTOs;
using EHT.BLL.Services.Concrete.OrganizationService;
using EHT.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EHT.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Organization")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;

        public OrganizationController(IOrganizationService organizationService, IMapper mapper)
        {
            _organizationService = organizationService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(OrganizationDto), 200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Get(int id)
        {
            var organization = await _organizationService.GetByIdAsync(id);

            return organization == null
                ? (ActionResult)NoContent()
                : Ok(organization);
        }

        [HttpPost]
        //[Authorize(Policy = "RequireLoggedIn")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<ActionResult> Create([FromBody] OrganizationToCreateModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel(400, "Invalid value was entered! Please, redisplay form."));
            }

            var organizationDto = _mapper.Map<OrganizationDto>(model);

            var result = await _organizationService.CreateOrUpdateAsync(organizationDto);

            return result.Succeeded
                ? Ok(new ResponseModel(200, "Completed.", "Organization created."))
                : (ActionResult)BadRequest(new ResponseModel(400, "Failed.", result.Error));
        }

        [HttpPut]
        //[Authorize(Policy = "RequireLoggedIn")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<ActionResult> Update([FromBody] OrganizationToUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel(400, "Invalid value was entered! Please, redisplay form."));
            }

            var organizationDto = _mapper.Map<OrganizationDto>(model);

            var result = await _organizationService.CreateOrUpdateAsync(organizationDto);

            return result.Succeeded
                ? Ok(new ResponseModel(200, "Completed.", "Organization updated."))
                : (ActionResult)BadRequest(new ResponseModel(400, "Failed.", result.Error));
        }

        [HttpDelete("{id}")]
        //[Authorize(Policy = "RequireLoggedIn")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _organizationService.DeleteAsync(id);

            return result.Succeeded
                ? Ok(new ResponseModel(200, "Completed.", "Organization deleted."))
                : (ActionResult)BadRequest(new ResponseModel(400, "Failed.", result.Error));
        }
    }
}