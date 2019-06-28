using EHT.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHT.BLL.Services.Concrete.DepartmentService
{
    public interface IDepartmentService
    {
        Task<DepartmentDto> GetByIdAsync(int departmentId);
        Task<ServiceResult> CreateOrUpdateAsync(DepartmentDto dto);
        Task<ServiceResult> DeleteAsync(int departmentId);
    }
}
