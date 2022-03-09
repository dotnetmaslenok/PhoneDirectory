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
    public class PhoneNumberService : IPhoneNumberService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public PhoneNumberService(IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<PhoneNumberDto> GetById(int phoneNumberId)
        {
            var phoneNumber = await _dbContext.PhoneNumbers
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == phoneNumberId);

            if (phoneNumber is null)
            {
                throw new PhoneNumberNotFoundException(phoneNumberId);
            }

            return _mapper.Map<PhoneNumberDto>(phoneNumber);
        }

        public async Task Create(CreatePhoneNumberDto phoneNumberDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == phoneNumberDto.UserId);

            if (user is null)
            {
                throw new UserNotFoundException(phoneNumberDto.UserId);
            }
            
            var phoneNumber = _mapper.Map<PhoneNumber>(phoneNumberDto);

            await _dbContext.PhoneNumbers.AddAsync(phoneNumber);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(UpdatePhoneNumberDto phoneNumberDto)
        {
            var phoneNumber = await _dbContext.PhoneNumbers
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == phoneNumberDto.Id);

            if (phoneNumber is null)
            {
                throw new PhoneNumberNotFoundException(phoneNumberDto.Id);
            }
            
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == phoneNumberDto.UserId);

            if (user is null)
            {
                throw new UserNotFoundException(phoneNumberDto.UserId);
            }
            
            phoneNumber.UserId = phoneNumberDto.UserId;
            phoneNumber.User = user;
            phoneNumber.UserPhoneNumber = phoneNumberDto.UserPhoneNumber;

            _dbContext.PhoneNumbers.Update(phoneNumber);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int phoneNumberId)
        {
            var phoneNumber = await _dbContext.PhoneNumbers.FirstOrDefaultAsync(x => x.Id == phoneNumberId);

            if (phoneNumber is null)
            {
                throw new PhoneNumberNotFoundException(phoneNumberId);
            }
            
            _dbContext.PhoneNumbers.Remove(phoneNumber);
            await _dbContext.SaveChangesAsync();
        }
    }
}