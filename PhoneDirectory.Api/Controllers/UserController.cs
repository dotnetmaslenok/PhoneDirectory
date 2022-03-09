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
        public async Task<IActionResult> GetUser([FromRoute] [Range(1, int.MaxValue)] int userId)
        {
            var user = await _userService.GetById(userId);

            if (user is null)
            {
                return NotFound();
            }
            
            return Ok(user);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetUsersByName([FromQuery(Name = "n")] [Required(AllowEmptyStrings = false)] string namePattern)
        {
            var users = await _userService.SearchByName(namePattern);

            if (users is null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet]
        [Route("set-chief")]
        public async Task<IActionResult> SetChief([FromQuery(Name = "u")] [Range(1, int.MaxValue)] int userId)
        {
	        await _userService.SetChief(userId);

	        return Ok();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            await _userService.Create(userDto);

            return Ok();
        }

        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDto)
        {
            await _userService.Update(userDto);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] [Range(1, int.MaxValue)] int userId)
        {
            await _userService.Delete(userId);

            return Ok();
        }
    }
}