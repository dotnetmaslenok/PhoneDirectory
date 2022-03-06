using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhoneDirectory.Application.Dtos;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;
using PhoneDirectory.Application.Interfaces;
using PhoneDirectory.Domain.CustomExceptions;
using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Infrastructure.Database;

namespace PhoneDirectory.Application.Services
{
    public class DivisionService : IDivisionService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public DivisionService(IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        
        public async Task<DivisionDto> GetById(int divisionId)
        {
            //TestDivision1
            //NestedDivision1
	        var division = await _dbContext.Divisions
                .Include(x => x.Users)
                .Include(x => x.Divisions)
                .FirstOrDefaultAsync(x => x.Id == divisionId);

            var divisionDto = _mapper.Map<DivisionDto>(division);
            return divisionDto;
        }

        public async Task<List<DivisionDto>> SearchByName(string namePattern)
        {
            var divisions = await _dbContext.Divisions
                .Where(x => x.Name.ToLower().Contains(namePattern.ToLower()))
                .Include(x => x.Users)
                .Include(x => x.Divisions)
                .ToListAsync();

            return _mapper.Map<List<DivisionDto>>(divisions);
        }

        public async Task Create(CreateDivisionDto divisionDto)
        {
            var division = _mapper.Map<Division>(divisionDto);

            await _dbContext.Divisions.AddAsync(division);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(UpdateDivisionDto divisionDto)
        {
            var division = await _dbContext.Divisions.FirstOrDefaultAsync(x => x.Id == divisionDto.Id);

            if (division is null)
            {
                throw new DivisionNotFoundException(divisionDto.Id);
            }

            division.Name = divisionDto.Name;

            _dbContext.Divisions.Update(division);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int divisionId)
        {
            var division = await _dbContext.Divisions.FirstOrDefaultAsync(x => x.Id == divisionId);

            if (division is null)
            {
                throw new DivisionNotFoundException(divisionId);
            }
            
            _dbContext.Divisions.Remove(division);
            await _dbContext.SaveChangesAsync();
        }
    }
}