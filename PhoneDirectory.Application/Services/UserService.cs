using System.Collections.Generic;
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
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public UserService(IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        
        public async Task<ApplicationUserDto> GetById(int userId)
        {
            var user = await _dbContext.Users
                .Include(x => x.Division)
                .Include(x => x.PhoneNumbers)
                .FirstOrDefaultAsync(x => x.Id == userId);

            return _mapper.Map<ApplicationUserDto>(user);
        }

        public async Task<List<ApplicationUserDto>> SearchByName(FilterDto filterDto)
        {
	        var users = new List<ApplicationUser>();

	        if (filterDto is not null && filterDto.ParentId.HasValue)
	        {
		        var parentDivision = await _dbContext.Divisions
			        .Include(x => x.Users)
			        .ThenInclude(x => x.PhoneNumbers)
			        .FirstOrDefaultAsync(x => x.Id == filterDto.ParentId);

		        if (!string.IsNullOrEmpty(filterDto.Name))
		        {
			        foreach (var user in parentDivision.Users)
			        {
				        if (user.Name.ToLower().Contains(filterDto.Name.ToLower()))
				        {
					        users.Add(user);
				        }
			        }
		        }
		        else
		        {
			        users.AddRange(parentDivision.Users);
		        }
	        }
	        else
	        {
		        users = await _dbContext.Users
			        .Include(x => x.Division)
			        .Include(x => x.PhoneNumbers)
			        .ToListAsync();
	        }

	        return _mapper.Map<List<ApplicationUserDto>>(users);
        }

        public async Task<int> Create(CreateUserDto userDto)
        {
            var division = await _dbContext.Divisions.FirstOrDefaultAsync(x => x.Id == userDto.DivisionId);
            
            if (division is null)
            {
                throw new DivisionNotFoundException(userDto.DivisionId);
            }
            
            var user = _mapper.Map<ApplicationUser>(userDto);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task Update(UpdateUserDto userDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userDto.Id);

            if (user is null)
            {
                throw new UserNotFoundException(userDto.Id);
            }

            var division = await _dbContext.Divisions.FirstOrDefaultAsync(x => x.Id == userDto.DivisionId);

            if (division is null)
            {
                throw new DivisionNotFoundException(userDto.DivisionId);
            }
            
            user.Name = userDto.Name;
            user.IsChief = user.IsChief;
            user.DivisionId = user.DivisionId;
            user.Division = division;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }
            
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SetChief(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }
            
            user.IsChief = true;

            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}