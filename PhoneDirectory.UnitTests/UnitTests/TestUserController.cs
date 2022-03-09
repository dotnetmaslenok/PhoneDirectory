using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Moq;
using PhoneDirectory.Api.Controllers;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;
using PhoneDirectory.Application.Interfaces;
using PhoneDirectory.Application.Services;
using PhoneDirectory.Domain.CustomExceptions;
using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Infrastructure.Database;
using PhoneDirectory.UnitTests.DataHelpers;
using PhoneDirectory.UnitTests.DtoHelpers;
using PhoneDirectory.UnitTests.Fixtures;
using Xunit;

namespace PhoneDirectory.UnitTests.UnitTests
{
    [Collection("Database collection")]
    public class TestUserController
    {
        private readonly DatabaseFixture _databaseFixture;
        private readonly Mock<IMapper> _mapper;
        private readonly IUserService _userService;
        private readonly UserController _userController;

        public TestUserController(DatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
            _mapper = new Mock<IMapper>();
            _userService = new UserService(_mapper.Object, _databaseFixture.DbContext);
            _userController = new UserController(_userService);
        }

        [Fact]
        public async Task CreateUser_ShouldThrowException_WhenDivisionNotFound()
        {
            // arrange
            var userDto = UserDtoHelper.GetOneInvalidCreateDto();
            
            // act
            // assert
            await Assert.ThrowsAsync<DivisionNotFoundException>(async () => await _userController.CreateUser(userDto));
        }
        
        [Fact]
        public async Task CreateUser_ShouldReturnOk_WhenUserCreated()
        {
            // arrange
            var user = UserHelper.GetOneCreatedEntity();
            var userDto = UserDtoHelper.GetOneCreateDto();
            _mapper.Setup(x => x.Map<ApplicationUser>(userDto)).Returns(user);
            var countBefore = _databaseFixture.DbContext.Users.Count();
            
            // act
            var result = await _userController.CreateUser(userDto);
            var countAfter = _databaseFixture.DbContext.Users.Count();
            
            // assert
            Assert.Equal(countBefore + 1, countAfter);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetUser_ShouldReturnNotFound_WhenUserNotFound()
        {
            // arrange
            var userId = int.MaxValue;
            ApplicationUser user = default;
            ApplicationUserDto userDto = default;
            _mapper.Setup(x => x.Map<ApplicationUserDto>(user)).Returns(userDto);
            
            // act
            var result = await _userController.GetUser(userId);

            // assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetUser_ShouldReturnOk_WhenUserFound()
        {
            // arrange
            var userId = 1;
            
            var user = await _databaseFixture.DbContext.Users
                .Include(x => x.Division)
                .Include(x => x.PhoneNumbers)
                .FirstOrDefaultAsync(x => x.Id == userId);
            
            var userDto = UserDtoHelper.GetOneDefaultDto();
            _mapper.Setup(x => x.Map<ApplicationUserDto>(user)).Returns(userDto);
            
            // act
            var result = await _userController.GetUser(userId);
            
            // assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PatchUser_ShouldThrowException_WhenUserNotFound()
        {
            // arrange
            UpdateUserDto userDto = UserDtoHelper.GetUpdateDtoWithInvalidId();

            // act
            // assert
            await Assert.ThrowsAsync<UserNotFoundException>(async () => await _userController.UpdateUser(userDto));
        }

        [Fact]
        public async Task PatchUser_ShouldThrowException_WhenDivisionNotFound()
        {
            // arrange
            UpdateUserDto userDto = UserDtoHelper.GetUpdateDtoWithInvalidDivisionId();

            // act
            // assert
            await Assert.ThrowsAsync<DivisionNotFoundException>(async () => await _userController.UpdateUser(userDto));
        }

        [Fact]
        public async Task PatchUser_ShouldReturnOk_WhenUserFound()
        {
            // arrange
            UpdateUserDto userDto = UserDtoHelper.GetOneUpdateDto();
            
            // act
            var result = await _userController.UpdateUser(userDto);
            var updatedUser = await _databaseFixture.DbContext.Users.FirstOrDefaultAsync(x => x.Id == userDto.Id);
            
            // assert
            Assert.Equal(updatedUser.Name, userDto.Name);
            Assert.Equal(updatedUser.IsChief, userDto.IsChief);
            Assert.Equal(updatedUser.DivisionId, userDto.DivisionId);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ShouldThrowException_WhenUserNotFound()
        {
            // arrange
            var userId = int.MaxValue;
            
            // act
            // assert
            await Assert.ThrowsAsync<UserNotFoundException>(async () => await _userController.DeleteUser(userId));
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnOk_WhenUserFound()
        {
            // arrange
            var userId = 4;
            var countBefore = _databaseFixture.DbContext.Users.Count();
            
            // act
            var result = await _userController.DeleteUser(userId);
            var countAfter = _databaseFixture.DbContext.Users.Count();

            // assert
            Assert.Equal(countBefore - 1, countAfter);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task SetChief_ShouldThrowException_WhenUserNotFound()
        {
            // arrange
            var userId = 3;

            // act
            var result = await _userController.SetChief(userId);
            var chiefUser = await _databaseFixture.DbContext.Users
                .Select(x => new {Id = x.Id, IsChief = x.IsChief})
                .FirstOrDefaultAsync(x => x.Id == userId);
            
            // assert
            Assert.True(chiefUser.IsChief);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task SearchUsers_ShouldReturnOk_WhenUsersFound()
        {
            // arrange
            var namePattern = "Test";

            var users = await _databaseFixture.DbContext.Users
                .Where(x => x.Name.ToLower().Contains(namePattern.ToLower()))
                .Include(x => x.Division)
                .Include(x => x.PhoneNumbers)
                .ToListAsync();

            var userDtos = UserDtoHelper.GetManyDefauldDtos();
            
            _mapper.Setup(x => x.Map<List<ApplicationUserDto>>(users)).Returns(userDtos);

            // act
            var result = await _userController.GetUsersByName(namePattern);
            
            // assert
            Assert.Equal(users.Count, userDtos.Count);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}