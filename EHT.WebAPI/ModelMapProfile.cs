using AutoMapper;
using EHT.BLL.DTOs;
using EHT.WebAPI.Models;

namespace EHT.WebAPI
{
    public class ModelMapProfile : Profile
    {
        public ModelMapProfile()
        {
            CreateMap<NodeToCreateModel, NodeDto>();
            CreateMap<NodeToUpdateModel, NodeDto>();
            CreateMap<NodeToDeleteModel, NodeDto>();
            CreateMap<RegisterModel, AppUserDto>();
        }
    }
}
