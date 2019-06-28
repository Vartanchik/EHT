using System;
using System.Threading.Tasks;
using AutoMapper;
using EHT.BLL.DTOs;
using EHT.DAL.Entities;
using EHT.DAL.UnitOfWork;

namespace EHT.BLL.Services.Concrete.BusinessService
{
    public class BusinessService : IBusinessService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BusinessService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BusinessDto> GetByIdAsync(int businessId)
        {
            try
            {
                var business = await _uow.Businesses.GetByIdAsync(businessId);

                return _mapper.Map<BusinessDto>(business);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ServiceResult> CreateOrUpdateAsync(BusinessDto dto)
        {
            try
            {
                var business = _mapper.Map<Business>(dto);

                await _uow.Businesses.CreateOrUpdate(business);
                await _uow.CommitAsync();

                return new ServiceResult();
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteAsync(int businessId)
        {
            try
            {
                await _uow.Businesses.DeleteAsync(businessId);
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
