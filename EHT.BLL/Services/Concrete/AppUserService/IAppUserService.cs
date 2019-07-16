using EHT.BLL.DTOs;
using System.Threading.Tasks;

namespace EHT.BLL.Services.Concrete.AppUserService
{
    public interface IAppUserService
    {
        Task<ServiceResult> CreateAsync(AppUserDto appUserDto, string password);
        Task<int> GetUserIdAsync(string email);
        Task<bool> CheckPasswordAsync(int userId, string password);
        string GenerateToken(int userId, string securityKey, string expireTime);
    }
}
