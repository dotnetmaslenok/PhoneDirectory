using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PhoneDirectory.Api.Controllers;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Interfaces;
using PhoneDirectory.Application.Services;
using PhoneDirectory.Domain.CustomExceptions;
using PhoneDirectory.Domain.Entities;
using PhoneDirectory.UnitTests.DataHelpers;
using PhoneDirectory.UnitTests.DtoHelpers;
using PhoneDirectory.UnitTests.Fixtures;
using Xunit;

namespace PhoneDirectory.UnitTests.UnitTests
{
    [Collection("Database collection")]
    public class TestPhoneNumberController
    {
        private readonly DatabaseFixture _databaseFixture;
        private readonly Mock<IMapper> _mapper;
        private readonly IPhoneNumberService _phoneNumberService;
        private readonly PhoneNumberController _phoneNumberController;

        public TestPhoneNumberController(DatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
            _mapper = new Mock<IMapper>();
            _phoneNumberService = new PhoneNumberService(_mapper.Object, _databaseFixture.DbContext);
            _phoneNumberController = new PhoneNumberController(_phoneNumberService);
        }

        [Fact]
        public async Task CreatePhoneNumber_ShouldThrowException_WhenUserNotFound()
        {
            // arrange
            var phoneNumberDto = PhoneNumberDtoHelper.GetOneInvalidCreateDto();
            
            // act
            var result = await _phoneNumberController.CreatePhoneNumber(phoneNumberDto);
            
            // assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public async Task CreatePhoneNumber_ShouldReturnOk_WhenPhoneNumberCreated()
        {
            // arrange
            var phoneNumber = PhoneNumberHelper.GetOneCreatedEntity();
            var phoneNumberDto = PhoneNumberDtoHelper.GetOneCreateDto();
            _mapper.Setup(x => x.Map<PhoneNumber>(phoneNumberDto)).Returns(phoneNumber);
            var countBefore = _databaseFixture.DbContext.PhoneNumbers.Count();
            
            // act
            var result = await _phoneNumberController.CreatePhoneNumber(phoneNumberDto);
            var countAfter = _databaseFixture.DbContext.PhoneNumbers.Count();

            // assert
            Assert.Equal(countBefore + 1, countAfter);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPhoneNumber_ShouldThrowException_WhenPhoneNumberNotFound()
        {
            // arrange
            var phoneNumberId = int.MaxValue;
            
            // act
            var result = await _phoneNumberController.GetPhoneNumber(phoneNumberId);

            // assert;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetPhoneNumber_ShouldReturnOk_WhenPhoneNumberFound()
        {
            // arrange
            var phoneNumberId = 1;
            
            var phoneNumber = await _databaseFixture.DbContext.PhoneNumbers
                .Include(x=>x.User)
                .FirstOrDefaultAsync(x=>x.Id == phoneNumberId);
            
            var phoneNumberDto = PhoneNumberDtoHelper.GetOne();
            _mapper.Setup(x => x.Map<PhoneNumberDto>(phoneNumber)).Returns(phoneNumberDto);
            
            // act
            var result = await _phoneNumberController.GetPhoneNumber(phoneNumberId);
            
            // assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PatchPhoneNumber_ShouldThrowException_WhenPhoneNumberNotFound()
        {
            // arrange
            var phoneNumberDto = PhoneNumberDtoHelper.GetOneUpdateDtoWithInvalidId();
            
            // act
            var result = await _phoneNumberController.UpdatePhoneNumber(phoneNumberDto);
            
            // assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PatchPhoneNumber_ShouldThrowException_WhenUserNotFound()
        {
            // arrange
            var phoneNumberDto = PhoneNumberDtoHelper.GetOneUpdateDtoWithInvalidUserId();
            
            // act
            var result = await _phoneNumberController.UpdatePhoneNumber(phoneNumberDto);
            
            // assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PatchPhoneNumber_ShouldReturnOk_WhenUserPhoneNumberFound()
        {
            // arrange
            var phoneNumberDto = PhoneNumberDtoHelper.GetOneUpdateDto();
            
            // act
            var result = await _phoneNumberController.UpdatePhoneNumber(phoneNumberDto);
            var updatedPhoneNumber = await _databaseFixture.DbContext.PhoneNumbers
                .Select(x=> new {Id = x.Id, UserPhoneNumber = x.UserPhoneNumber, UserId = x.UserId})
                .FirstOrDefaultAsync(x => x.Id == phoneNumberDto.Id);

            // assert
            Assert.Equal(updatedPhoneNumber.UserPhoneNumber, phoneNumberDto.UserPhoneNumber);
            Assert.Equal(updatedPhoneNumber.UserId, phoneNumberDto.UserId);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeletePhoneNumber_ShouldThrowException_WhenPhoneNumberNotFound()
        {
            // arrange
            var phoneNumberId = int.MaxValue;
            
            // act
            var result = await _phoneNumberController.DeletePhoneNumber(phoneNumberId);
            
            // assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeletePhoneNumber_ShouldReturnOk_WhenPhoneNumberDeleted()
        {
            // arrange
            var phoneNumberId = 4;
            var countBefore = _databaseFixture.DbContext.PhoneNumbers.Count();
            
            // act
            var result = await _phoneNumberController.DeletePhoneNumber(phoneNumberId);
            var countAfter = _databaseFixture.DbContext.PhoneNumbers.Count();
            
            // assert
            Assert.Equal(countBefore - 1, countAfter);
            Assert.IsType<OkResult>(result);
        }
    }
}