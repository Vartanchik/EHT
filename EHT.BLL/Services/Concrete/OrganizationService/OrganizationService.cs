using System;
using System.Threading.Tasks;
using AutoMapper;
using EHT.BLL.DTOs;
using EHT.DAL.Entities;
using EHT.DAL.UnitOfWork;

namespace EHT.BLL.Services.Concrete.OrganizationService
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrganizationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrganizationDto> GetByIdAsync(int organizationId)
        {
            try
            {
                var organization = await _uow.Organizations.GetByIdAsync(organizationId);

                return _mapper.Map<OrganizationDto>(organization);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ServiceResult> CreateOrUpdateAsync(OrganizationDto dto)
        {
            try
            {
                var organization = _mapper.Map<Organization>(dto);

                await _uow.Organizations.CreateOrUpdate(organization);
                await _uow.CommitAsync();

                return new ServiceResult();
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteAsync(int organizationId)
        {
            try
            {
                await _uow.Organizations.DeleteAsync(organizationId);
                await _uow.CommitAsync();

                return new ServiceResult();
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex.Message);
            }
        }

    }
}
