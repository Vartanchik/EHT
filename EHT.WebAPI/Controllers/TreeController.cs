using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EHT.BLL.DTOs;
using EHT.BLL.Services.Concrete.OrganizationService;
using EHT.BLL.Services.Concrete.TreeService;
using EHT.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHT.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Tree")]
    [ApiController]
    public class TreeController : ControllerBase
    {
        private readonly ITreeService _treeService;
        private readonly IMapper _mapper;

        public TreeController(ITreeService treeService, IMapper mapper)
        {
            _treeService = treeService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IList<NodeDto>), 200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<IList<NodeDto>>> Get()
        {
            var listOfNodes = await _treeService.GetTreeAsync();

            return listOfNodes == null
                ? (ActionResult)NoContent()
                : Ok(listOfNodes);
        }

        [Route("CreateNode")]
        [HttpPost]
        //[Authorize(Policy = "RequireLoggedIn")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<ActionResult> CreateNode([FromBody] NodeToCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel(400, "Invalid value was entered! Please, redisplay form."));
            }

            var nodeDto = _mapper.Map<NodeDto>(model);

            var result = await _treeService.CreateNodeAsync(nodeDto);

            return result.Succeeded
                ? Ok(new ResponseModel(200, "Completed.", "Node created."))
                : (ActionResult)BadRequest(new ResponseModel(400, "Failed.", result.Error));
        }

        [Route("UpdateNode")]
        [HttpPut]
        //[Authorize(Policy = "RequireLoggedIn")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<ActionResult> UpdateNode([FromBody] NodeToUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel(400, "Invalid value was entered! Please, redisplay form."));
            }

            var nodeDto = _mapper.Map<NodeDto>(model);

            var result = await _treeService.UpdateNodeAsync(nodeDto);

            return result.Succeeded
                ? Ok(new ResponseModel(200, "Completed.", "Node updated."))
                : (ActionResult)BadRequest(new ResponseModel(400, "Failed.", result.Error));
        }

        [Route("DeleteNode")]
        [HttpDelete]
        //[Authorize(Policy = "RequireLoggedIn")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<ActionResult> DeleteNode([FromBody] NodeToDeleteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel(400, "Invalid value was entered! Please, redisplay form."));
            }

            var nodeDto = _mapper.Map<NodeDto>(model);

            var result = await _treeService.DeleteNodeAsync(nodeDto);

            return result.Succeeded
                ? Ok(new ResponseModel(200, "Completed.", "Node deleted."))
                : (ActionResult)BadRequest(new ResponseModel(400, "Failed.", result.Error));
        }
    }
}