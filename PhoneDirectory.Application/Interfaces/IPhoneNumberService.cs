using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;

namespace PhoneDirectory.Application.Interfaces
{
    public interface IPhoneNumberService
    {
        public Task<PhoneNumberDto> GetById(int phoneNumberId);

        public Task<List<PhoneNumberDto>> GetAll();

        public Task<int> Create(CreatePhoneNumberDto phoneNumberDto);

        public Task Update(UpdatePhoneNumberDto phoneNumberDto);

        public Task Delete(int phoneNumberId);
	}
}