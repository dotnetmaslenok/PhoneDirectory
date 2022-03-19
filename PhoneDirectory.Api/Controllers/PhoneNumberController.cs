using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;
using PhoneDirectory.Application.Interfaces;
using PhoneDirectory.Domain.BaseExceptions;
using PhoneDirectory.Domain.CustomExceptions;

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
        [ProducesResponseType(typeof(PhoneNumberDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPhoneNumber([FromRoute] [Range(1, int.MaxValue)] int phoneNumberId)
        {
            try
            {
                var phoneNumber = await _phoneNumberService.GetById(phoneNumberId);
                return Ok(phoneNumber);
            }
            catch (PhoneNumberNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PhoneNumberDto>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetPhoneNumbers()
        {
	        var phoneNumbers = await _phoneNumberService.GetAll();

            return Ok(phoneNumbers);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(int) ,(int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreatePhoneNumber([FromBody] CreatePhoneNumberDto phoneNumberDto)
        {
            try
            {
                var phoneNumberId = await _phoneNumberService.Create(phoneNumberDto);
                return Ok(phoneNumberId);
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePhoneNumber([FromBody] UpdatePhoneNumberDto phoneNumberDto)
        {
            try
            {
                await _phoneNumberService.Update(phoneNumberDto);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{phoneNumberId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletePhoneNumber([FromRoute] [Range(1, int.MaxValue)] int phoneNumberId)
        {
            try
            {
                await _phoneNumberService.Delete(phoneNumberId);
                return Ok();
            }
            catch (PhoneNumberNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}