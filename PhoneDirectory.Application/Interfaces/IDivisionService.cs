using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.FilterDtos;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;

namespace PhoneDirectory.Application.Interfaces
{
    public interface IDivisionService
    {
        public Task<DivisionDto> GetById(int divisionId);

        public Task<List<DivisionDto>> SearchByName(FilterDto filterDto);

        public Task<int> Create(CreateDivisionDto divisionDto);

        public Task Update(UpdateDivisionDto divisionDto);

        public Task Delete(int divisionId);
    }
}