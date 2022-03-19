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
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(typeof(ApplicationUserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUser([FromRoute] [Range(1, int.MaxValue)] int userId)
        {
            try
            {
                var user = await _userService.GetById(userId);
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        [Route("set-chief")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> SetChief([FromQuery(Name = "u")] [Range(1, int.MaxValue)] int userId)
        {
            try
            {
                await _userService.SetChief(userId);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("search")]
        [ProducesResponseType(typeof(List<ApplicationUserDto>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsersByName([FromBody] FilterDto filterDto)
        {
            var users = await _userService.SearchByName(filterDto);

            return Ok(users);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int) ,(int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            try
            {
                var userId = await _userService.Create(userDto);
                return Ok(userId);
            }
            catch (DivisionNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDto)
        {
            try
            {
                await _userService.Update(userDto);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUser([FromRoute] [Range(1, int.MaxValue)] int userId)
        {
            try
            {
                await _userService.Delete(userId);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}