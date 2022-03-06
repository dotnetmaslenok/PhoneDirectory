using AutoMapper;
using PhoneDirectory.Application.Dtos;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Division, DivisionDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Divisions, opt => opt.MapFrom(x => x.Divisions))
                .ForMember(x => x.Users, opt => opt.MapFrom(x => x.Users))
                .MaxDepth(1)
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.IsChief, opt => opt.MapFrom(x => x.IsChief))
                .ForMember(x => x.DivisionId, opt => opt.MapFrom(x => x.DivisionId))
                .ForMember(x => x.Division, opt => opt.MapFrom(x => x.Division))
                .ForMember(x => x.PhoneNumbers, opt => opt.MapFrom(x => x.PhoneNumbers))
                .MaxDepth(1)
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<PhoneNumber, PhoneNumberDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.UserPhoneNumber, opt => opt.MapFrom(x => x.UserPhoneNumber))
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId))
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.User))
                .MaxDepth(1)
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CreateDivisionDto, Division>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Divisions, opt => opt.MapFrom(x => x.Divisions))
                .MaxDepth(1)
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CreateUserDto, ApplicationUser>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.DivisionId, opt => opt.MapFrom(x => x.DivisionId))
                .ForMember(x => x.IsChief, opt => opt.MapFrom(x => x.IsChief))
                .MaxDepth(1)
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CreatePhoneNumberDto, PhoneNumber>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId))
                .ForMember(x => x.UserPhoneNumber, opt => opt.MapFrom(x => x.UserPhoneNumber))
                .MaxDepth(1)
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<UpdateDivisionDto, Division>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .MaxDepth(1)
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<UpdateUserDto, ApplicationUser>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.IsChief, opt => opt.MapFrom(x => x.IsChief))
                .ForMember(x => x.DivisionId, opt => opt.MapFrom(x => x.DivisionId))
                .MaxDepth(1)
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<UpdatePhoneNumberDto, PhoneNumber>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId))
                .ForMember(x => x.UserPhoneNumber, opt => opt.MapFrom(x => x.UserPhoneNumber))
                .MaxDepth(1)
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}