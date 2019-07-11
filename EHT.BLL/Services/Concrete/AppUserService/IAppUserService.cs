using EHT.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHT.BLL.Services.Concrete.AppUserService
{
    public interface IAppUserService
    {
        Task<ServiceResult> CreateAsync(AppUserDto dto, string password);
    }
}
