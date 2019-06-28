using EHT.BLL.DTOs;
using System.Threading.Tasks;

namespace EHT.BLL.Services.Concrete.OrganizationService
{
    public interface IOrganizationService
    {
        Task<OrganizationDto> GetByIdAsync(int organizationId);
        Task<ServiceResult> CreateOrUpdateAsync(OrganizationDto dto);
        Task<ServiceResult> DeleteAsync(int organizationId);
    }
}
