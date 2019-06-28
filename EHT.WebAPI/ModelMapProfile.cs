using AutoMapper;
using EHT.BLL.DTOs;
using EHT.WebAPI.Models;

namespace EHT.WebAPI
{
    public class ModelMapProfile : Profile
    {
        public ModelMapProfile()
        {
            CreateMap<OrganizationToCreateModel, OrganizationDto>();
            CreateMap<OrganizationToUpdateModel, OrganizationDto>();
            CreateMap<NodeToCreateModel, NodeDto>();
            CreateMap<NodeToUpdateModel, NodeDto>();
            CreateMap<NodeToDeleteModel, NodeDto>();
            CreateMap<CountryToCreateModel, CountryDto>()
                .ForPath(dest => dest.Organization.Id,
                           config => config.MapFrom(src => src.OrganizationId));
            CreateMap<CountryToUpdateModel, CountryDto>();
        }
    }
}
