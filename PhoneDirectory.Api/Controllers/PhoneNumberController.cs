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
    [Route("api/phoneNumber")]
    public class PhoneNumberController : ControllerBase
    {
        private readonly IPhoneNumberService _phoneNumberService;

        public PhoneNumberController(IPhoneNumberService phoneNumberService)
        {
            _phoneNumberService = phoneNumberService;
        }

        [HttpGet]
        [Route("{phoneNumberId}")]
        public async Task<IActionResult> GetPhoneNumber([FromRoute] [Range(1, int.MaxValue)] int phoneNumberId)
        {
            var phoneNumber = await _phoneNumberService.GetById(phoneNumberId);

            if (phoneNumber is null)
            {
                return NotFound();
            }
            
            return Ok(phoneNumber);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePhoneNumber([FromBody] CreatePhoneNumberDto phoneNumberDto)
        {
            await _phoneNumberService.Create(phoneNumberDto);

            return Ok();
        }

        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdatePhoneNumber([FromBody] UpdatePhoneNumberDto phoneNumberDto)
        {
            await _phoneNumberService.Update(phoneNumberDto);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{phoneNumberId}")]
        public async Task<IActionResult> DeletePhoneNumber([FromRoute] [Range(1, int.MaxValue)] int phoneNumberId)
        {
            await _phoneNumberService.Delete(phoneNumberId);

            return Ok();
        }
    }
}