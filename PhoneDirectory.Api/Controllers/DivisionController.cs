using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.FilterDtos;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;
using PhoneDirectory.Application.Interfaces;
using PhoneDirectory.Domain.CustomExceptions;

namespace PhoneDirectory.Api.Controllers
{
    [ApiController]
    [Route("api/division")]
    public class DivisionController : ControllerBase
    {
        private readonly IDivisionService _divisionService;

        public DivisionController(IDivisionService divisionService)
        {
            _divisionService = divisionService;
        }

        [HttpGet]
        [Route("{divisionId}")]
        [ProducesResponseType(typeof(DivisionDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDivision([FromRoute] [Range(1, int.MaxValue)] int divisionId)
        {

	        try
	        { 
		        var division = await _divisionService.GetById(divisionId);
		        return Ok(division);
	        }
	        catch (DivisionNotFoundException ex)
	        {
		        return BadRequest(ex.Message);
	        }
        }

        [HttpPost]
        [Route("search")]
        [ProducesResponseType(typeof(List<DivisionDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDivisionsByName([FromBody] FilterDto filterDto)
        {
            var divisions = await _divisionService.SearchByName(filterDto);

            return Ok(divisions);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateDivision([FromBody] CreateDivisionDto divisionDto)
        {
            var divisionId = await _divisionService.Create(divisionDto);

            return Ok(divisionId);
        }

        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateDivision([FromBody] UpdateDivisionDto divisionDto)
        {
	        try
	        {
		        await _divisionService.Update(divisionDto);
		        return Ok();
	        }
	        catch (DivisionNotFoundException ex)
	        {
		        return BadRequest(ex.Message);
	        }
        }

        [HttpDelete]
        [Route("{divisionId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteDivision([FromRoute] [Range(1, int.MaxValue)] int divisionId)
        {
	        try
	        {
		        await _divisionService.Delete(divisionId);
		        return Ok();
	        }
	        catch (DivisionNotFoundException ex)
	        {
		        return BadRequest(ex.Message);
	        }
        }
    }
}