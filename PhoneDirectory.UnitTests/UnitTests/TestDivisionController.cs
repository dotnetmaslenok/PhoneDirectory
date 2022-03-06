using System;
using System.Collections.Generic;
using System.Linq;
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
using Xunit;

namespace PhoneDirectory.UnitTests.UnitTests
{
    public class TestDivisionController
    {
        private static readonly ApplicationDbContext _context = new DbContextHelper().Context;
        private readonly Mock<IMapper> _mapper;
        private readonly IDivisionService _divisionService;
        private readonly DivisionController _controller;

        public TestDivisionController()
        {
            _mapper = new Mock<IMapper>();
            _divisionService = new DivisionService(_mapper.Object, _context);
            _controller = new DivisionController(_divisionService);
        }

        [Fact]
        public async Task CreateCompoundDivision_ShouldReturnOk_WhenDivisionsCreated()
        {
            // arrange
            var division = DivisionHelper.GetOne();
            var divisionDto = DivisionDtoHelper.GetOneCreateDto();
            _mapper.Setup(x => x.Map<Division>(divisionDto)).Returns(division);
            var countBefore = _context.Divisions.Count();
            
            // act
            var result = await _controller.CreateDivision(divisionDto);
            var countAfter = _context.Divisions.Count();

            // assert
            Assert.Equal(countBefore + 2, countAfter);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetDivision_ShouldReturnNotFound_WhenDivisionNotFound()
        {
            // arrange
            var divisionId = int.MaxValue;
            Division division = null;
            DivisionDto divisionDto = null;
            _mapper.Setup(x => x.Map<DivisionDto>(division)).Returns(divisionDto);
            
            // act
            var result = await _controller.GetDivision(divisionId);
            
            // assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PatchDivision_ShouldReturnNotFound_WhenDivisionNotFound()
        {
            // arrange
            var division = DivisionHelper.GetOne();
            var divisionDto = DivisionDtoHelper.GetOneInvalidUpdateDto();
            _mapper.Setup(x => x.Map<UpdateDivisionDto>(division)).Returns(divisionDto);

            // act
            // assert
            await Assert.ThrowsAsync<DivisionNotFoundException>(async () => await _controller.UpdateDivision(divisionDto));
        }
        
        [Fact]
        public async Task GetDivision_ShouldReturnOk_WhenDivisionFound()
        {
            // arrange
            var divisionId = 1;

            var division = await _context.Divisions
                .Include(x => x.Divisions)
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == divisionId);
            
            var divisionDto = DivisionDtoHelper.GetOneDefaultDto();
            
            _mapper.Setup(x => x.Map<DivisionDto>(division)).Returns(divisionDto);
            
            // act
            var result = await _controller.GetDivision(divisionId);
            
            // assert
            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public async Task PatchDivision_ShouldReturnOk_WhenDivisionFound()
        {
            var divisionId = 1;
            var division = await _context.Divisions.FirstOrDefaultAsync(x => x.Id == divisionId);
            var divisionDto = DivisionDtoHelper.GetOneUpdateDto();
            _mapper.Setup(x => x.Map<Division>(divisionDto)).Returns(division);
            
            // act
            var result = await _controller.UpdateDivision(divisionDto);
            var updatedDivision = await _context.Divisions.FirstOrDefaultAsync(x => x.Id == divisionId);
            
            // assert
            Assert.Equal(updatedDivision.Name, divisionDto.Name);
            Assert.IsType<OkResult>(result);
        }
    }
}