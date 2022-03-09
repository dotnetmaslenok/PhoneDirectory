using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
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
    public class TestDivisionController
    {
        private readonly DatabaseFixture _databaseFixture;
        private readonly Mock<IMapper> _mapper;
        private readonly IDivisionService _divisionService;
        private readonly DivisionController _divisionController;

        public TestDivisionController(DatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
            _mapper = new Mock<IMapper>();
            _divisionService = new DivisionService(_mapper.Object, _databaseFixture.DbContext);
            _divisionController = new DivisionController(_divisionService);
        }

        [Fact]
        public async Task CreateCompoundDivision_ShouldReturnOk_WhenDivisionsCreated()
        {
            // arrange
            var division = DivisionHelper.GetOneDefaultEntity();
            division.Name = "CreatedDivisionDto1";
            var divisionDto = DivisionDtoHelper.GetOneCreateDto();
            _mapper.Setup(x => x.Map<Division>(divisionDto)).Returns(division);
            var countBefore = _databaseFixture.DbContext.Divisions.Count();
            
            // act
            var result = await _divisionController.CreateDivision(divisionDto);
            var countAfter = _databaseFixture.DbContext.Divisions.Count();

            // assert
            Assert.Equal(countBefore + 2, countAfter);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetDivision_ShouldReturnNotFound_WhenDivisionNotFound()
        {
            // arrange
            var divisionId = int.MaxValue;
            Division division = default;
            DivisionDto divisionDto = default;
            _mapper.Setup(x => x.Map<DivisionDto>(division)).Returns(divisionDto);
            
            // act
            var result = await _divisionController.GetDivision(divisionId);
            
            // assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetDivision_ShouldReturnOk_WhenDivisionFound()
        {
            // arrange
            var divisionId = 1;

            var division = await _databaseFixture.DbContext.Divisions
                .Include(x => x.Divisions)
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == divisionId);
            
            var divisionDto = DivisionDtoHelper.GetOneDefaultDto();
            
            _mapper.Setup(x => x.Map<DivisionDto>(division)).Returns(divisionDto);
            
            // act
            var result = await _divisionController.GetDivision(divisionId);
            
            // assert
            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public async Task PatchDivision_ShouldThrowException_WhenDivisionNotFound()
        {
            // arrange
            var division = DivisionHelper.GetOneDefaultEntity();
            var divisionDto = DivisionDtoHelper.GetOneInvalidUpdateDto();
            _mapper.Setup(x => x.Map<UpdateDivisionDto>(division)).Returns(divisionDto);

            // act
            // assert
            await Assert.ThrowsAsync<DivisionNotFoundException>(async () => await _divisionController.UpdateDivision(divisionDto));
        }
        
        [Fact]
        public async Task PatchDivision_ShouldReturnOk_WhenDivisionFound()
        {
            // arrange
            var divisionId = 2;
            var division = await _databaseFixture.DbContext.Divisions.FirstOrDefaultAsync(x => x.Id == divisionId);
            var divisionDto = DivisionDtoHelper.GetOneUpdateDto();
            _mapper.Setup(x => x.Map<Division>(divisionDto)).Returns(division);
            var countBefore = _databaseFixture.DbContext.Divisions.Count();
            
            // act
            var result = await _divisionController.UpdateDivision(divisionDto);
            var updatedDivision = await _databaseFixture.DbContext.Divisions
                .Select(x=> new {Id = x.Id, Name = x.Name})
                .FirstOrDefaultAsync(x => x.Id == divisionId);
            var countAfter = _databaseFixture.DbContext.Divisions.Count();
            
            // assert
            Assert.Equal(updatedDivision.Name, divisionDto.Name);
            Assert.Equal(countBefore, countAfter);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteDivision_ShouldThrowException_WhenDivisionNotFound()
        {
            // arrange
            var divisionId = int.MaxValue;
            
            // act
            // assert
            await Assert.ThrowsAsync<DivisionNotFoundException>(async () => await _divisionController.DeleteDivision(divisionId));
        }

        [Fact]
        public async Task DeleteDivision_ShouldReturnOk_WhenDivisionFound()
        {
            // arrange
            var divisionId = 6;
            var countBefore = _databaseFixture.DbContext.Divisions.Count();

            // act
            var result = await _divisionController.DeleteDivision(divisionId);
            var countAfter = _databaseFixture.DbContext.Divisions.Count();
            
            // assert
            Assert.Equal(countBefore - 1, countAfter);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task SearchByDivisions_ShouldReturnNotFound_WhenDivisionsNotFound()
        {
            // arrange
            var namePattern = "Test";

            var divisions = await _databaseFixture.DbContext.Divisions
                .Where(x => x.Name.ToLower().Contains(namePattern.ToLower()))
                .Include(x => x.Divisions)
                .Include(x => x.Users)
                .ToListAsync();
            
            var divisionDtos = DivisionDtoHelper.GetManyDefaultDtos();
            _mapper.Setup(x => x.Map<List<DivisionDto>>(divisions)).Returns(divisionDtos);
            
            // act
            var result = await _divisionController.GetDivisionsByName(namePattern);
            
            // assert
            Assert.Equal(5, divisions.Count);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}