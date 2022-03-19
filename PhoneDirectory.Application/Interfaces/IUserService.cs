using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.FilterDtos;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;

namespace PhoneDirectory.Application.Interfaces
{
    public interface IUserService
    {
        public Task<ApplicationUserDto> GetById(int userId);

        public Task<List<ApplicationUserDto>> SearchByName(FilterDto filterDto);

        public Task<int> Create(CreateUserDto userDto);

        public Task Update(UpdateUserDto userDto);

        public Task Delete(int userId);

        public Task SetChief(int userId);
    }
}