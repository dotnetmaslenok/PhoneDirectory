using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.FilterDtos;
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
	        var division = await _dbContext.Divisions
		        .Include(x => x.ParentDivision)
		        .Include(x => x.ChildDivisions)
		        .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == divisionId);

	        if (division is null)
	        {
		        throw new DivisionNotFoundException(divisionId);
	        }

            var divisionDto = _mapper.Map<DivisionDto>(division);

            return divisionDto;
        }

        public async Task<List<DivisionDto>> SearchByName(FilterDto filterDto)
        {
            var divisions = new List<Division>();
            
            if (filterDto is not null && filterDto.ParentId.HasValue)
            {
	            var parentDivision = await _dbContext.Divisions
		            .Include(x => x.ChildDivisions)
		            .FirstOrDefaultAsync(x => x.Id == filterDto.ParentId);

                if (!string.IsNullOrEmpty(filterDto.Name))
	            {
		            foreach (var division in parentDivision.ChildDivisions)
		            {
			            if (division.Name.ToLower().Contains(filterDto.Name.ToLower()))
			            {
				            divisions.Add(division);
			            }
		            }
                }
	            else
	            {
		            divisions.AddRange(parentDivision.ChildDivisions);
	            }
            }
            else
            {
                divisions = await _dbContext.Divisions
	                .Include(x => x.ParentDivision)
	                .Include(x => x.ChildDivisions)
	                .Include(x => x.Users)
	                .ToListAsync();
            }
            
            return _mapper.Map<List<DivisionDto>>(divisions);
        }

        public async Task<int> Create(CreateDivisionDto divisionDto)
        {
	        var division = _mapper.Map<Division>(divisionDto);

	        if (divisionDto.ParentId.HasValue)
            {
                var parentDivision = await _dbContext.Divisions
                    .Include(x => x.ParentDivision)
                    .FirstOrDefaultAsync(x => x.Id == divisionDto.ParentId);

                if (division.ChildDivisions.Count > 0)
                {
	                var divisions = new List<Division> {division};

	                foreach (var childDivision in divisions.ToList())
	                {
		                parentDivision.ChildDivisions.Add(childDivision);
		                division.ParentDivision = parentDivision;
	                }
                }
	        }

            await _dbContext.Divisions.AddAsync(division);
            await _dbContext.SaveChangesAsync();

            return division.Id;
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