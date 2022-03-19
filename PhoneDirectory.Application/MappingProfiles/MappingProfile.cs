using AutoMapper;
using PhoneDirectory.Application.BaseDtos;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;
using PhoneDirectory.Domain.BaseEntities;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
	        CreateMap<BaseEntity, BaseDto>()
		        .Include<Division, DivisionDto>()
		        .Include<ApplicationUser, ApplicationUserDto>()
		        .Include<PhoneNumber, PhoneNumberDto>();

	        CreateMap<BaseDto, BaseEntity>()
		        .Include<UpdateDivisionDto, Division>()
		        .Include<UpdateUserDto, ApplicationUser>()
		        .Include<UpdatePhoneNumberDto, PhoneNumber>();

            CreateMap<Division, DivisionDto>()
	            .MaxDepth(2);

	        CreateMap<ApplicationUser, ApplicationUserDto>()
		        .MaxDepth(2);

	        CreateMap<PhoneNumber, PhoneNumberDto>()
		        .MaxDepth(2);

	        CreateMap<CreateDivisionDto, Division>();

            CreateMap<CreateUserDto, ApplicationUser>();

            CreateMap<CreatePhoneNumberDto, PhoneNumber>();

            CreateMap<UpdateDivisionDto, Division>();

            CreateMap<UpdateUserDto, ApplicationUser>();

            CreateMap<UpdatePhoneNumberDto, PhoneNumber>();
        }
    }
}