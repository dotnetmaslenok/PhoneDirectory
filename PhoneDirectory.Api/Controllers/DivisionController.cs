using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Application.Dtos;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;
using PhoneDirectory.Application.Interfaces;

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
        public async Task<IActionResult> GetDivision([FromRoute] [Range(1, int.MaxValue)] int divisionId)
        {
            var division = await _divisionService.GetById(divisionId);

            if (division is null)
            {
                return NotFound();
            }
            
            return Ok(division);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetDivisionsByName([FromQuery(Name = "n")] [Required(AllowEmptyStrings = false)] string namePattern)
        {
            var divisions = await _divisionService.SearchByName(namePattern);

            if (divisions is null)
            {
                return NotFound();
            }

            return Ok(divisions);
        }
        
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateDivision([FromBody] CreateDivisionDto divisionDto)
        {
            await _divisionService.Create(divisionDto);

            return Ok();
        }

        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateDivision([FromBody] UpdateDivisionDto divisionDto)
        {
            await _divisionService.Update(divisionDto);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{divisionId}")]
        public async Task<IActionResult> DeleteDivision([FromRoute] [Range(1, int.MaxValue)] int divisionId)
        {
            await _divisionService.Delete(divisionId);

            return Ok();
        }
    }
}