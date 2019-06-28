using EHT.BLL.DTOs;
using System.Threading.Tasks;

namespace EHT.BLL.Services.Concrete.CountryService
{
    public interface ICountryService
    {
        Task<CountryDto> GetByIdAsync(int countryId);
        Task<ServiceResult> CreateOrUpdateAsync(CountryDto dto);
        Task<ServiceResult> DeleteAsync(int countryId);
    }
}
