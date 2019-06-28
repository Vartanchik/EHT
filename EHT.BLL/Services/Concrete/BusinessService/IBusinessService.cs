using EHT.BLL.DTOs;
using System.Threading.Tasks;

namespace EHT.BLL.Services.Concrete.BusinessService
{
    public interface IBusinessService
    {
        Task<BusinessDto> GetByIdAsync(int businessId);
        Task<ServiceResult> CreateOrUpdateAsync(BusinessDto dto);
        Task<ServiceResult> DeleteAsync(int businessId);
    }
}
