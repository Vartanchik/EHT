using System;
using System.Threading.Tasks;
using AutoMapper;
using EHT.BLL.DTOs;
using EHT.DAL.Entities;
using EHT.DAL.UnitOfWork;

namespace EHT.BLL.Services.Concrete.CountryService
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CountryDto> GetByIdAsync(int countryId)
        {
            try
            {
                var country = await _uow.Countries.GetByIdAsync(countryId);

                return _mapper.Map<CountryDto>(country);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ServiceResult> CreateOrUpdateAsync(CountryDto dto)
        {
            try
            {
                var country = _mapper.Map<Country>(dto);

                await _uow.Countries.CreateOrUpdate(country);
                await _uow.CommitAsync();

                return new ServiceResult();
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteAsync(int countryId)
        {
            try
            {
                await _uow.Countries.DeleteAsync(countryId);
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
