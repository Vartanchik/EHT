using AutoMapper;
using EHT.BLL.DTOs;
using EHT.DAL.Entities;

namespace EHT.BLL.Services
{
    public class DtoMapProfile : Profile
    {
        public DtoMapProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Organization, OrganizationDto>();
            CreateMap<OrganizationDto, Organization>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Business, BusinessDto>();
            CreateMap<BusinessDto, Business>();
            CreateMap<Family, FamilyDto>();
            CreateMap<FamilyDto, Family>();
            CreateMap<Offering, OfferingDto>();
            CreateMap<OfferingDto, Offering>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();

            CreateMap<Organization, NodeDto>()
                .ForMember(dest => dest.Type,
                           config => config.MapFrom(src => src.GetType().Name))
                .ForMember(dest => dest.ParentId,
                           config => config.MapFrom(src => 0))
                .ForMember(dest => dest.Properties,
                           config => config.MapFrom(src => new NodePropertiesDto
                           {
                               Code = src.Code,
                               OrganizationType = src.OrganizationType,
                               OrganizationOwner = src.Owner
                           }));

            CreateMap<NodeDto, Organization>()
                .ForMember(dest => dest.Code,
                           config => config.MapFrom(src => src.Properties.Code))
                .ForMember(dest => dest.OrganizationType,
                           config => config.MapFrom(src => src.Properties.OrganizationType))
                .ForMember(dest => dest.Owner,
                           config => config.MapFrom(src => src.Properties.OrganizationOwner));

            CreateMap<Country, NodeDto>()
                .ForMember(dest => dest.Type,
                           config => config.MapFrom(src => src.GetType().Name))
                .ForMember(dest => dest.ParentId,
                           config => config.MapFrom(src => src.OrganizationId))
                .ForMember(dest => dest.Properties,
                           config => config.MapFrom(src => new NodePropertiesDto
                           {
                               Code = src.Code
                           }));

            CreateMap<NodeDto, Country>()
                .ForMember(dest => dest.OrganizationId,
                           config => config.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Code,
                           config => config.MapFrom(src => src.Properties.Code));

            CreateMap<Business, NodeDto>()
                .ForMember(dest => dest.Type,
                           config => config.MapFrom(src => src.GetType().Name))
                .ForMember(dest => dest.ParentId,
                           config => config.MapFrom(src => src.CountryId));

            CreateMap<NodeDto, Business>()
                .ForMember(dest => dest.CountryId,
                           config => config.MapFrom(src => src.ParentId));

            CreateMap<Family, NodeDto>()
                .ForMember(dest => dest.Type,
                           config => config.MapFrom(src => src.GetType().Name))
                .ForMember(dest => dest.ParentId,
                           config => config.MapFrom(src => src.BusinessId));

            CreateMap<NodeDto, Family>()
                .ForMember(dest => dest.BusinessId,
                           config => config.MapFrom(src => src.ParentId));

            CreateMap<Offering, NodeDto>()
                .ForMember(dest => dest.Type,
                           config => config.MapFrom(src => src.GetType().Name))
                .ForMember(dest => dest.ParentId,
                           config => config.MapFrom(src => src.FamilyId));

            CreateMap<NodeDto, Offering>()
                .ForMember(dest => dest.FamilyId,
                           config => config.MapFrom(src => src.ParentId));

            CreateMap<Department, NodeDto>()
                .ForMember(dest => dest.Type,
                           config => config.MapFrom(src => src.GetType().Name))
                .ForMember(dest => dest.ParentId,
                           config => config.MapFrom(src => src.OfferingId));

            CreateMap<NodeDto, Department>()
                .ForMember(dest => dest.OfferingId,
                           config => config.MapFrom(src => src.ParentId));

        }
    }
}
