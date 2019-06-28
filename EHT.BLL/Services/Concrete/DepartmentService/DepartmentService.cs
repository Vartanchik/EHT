using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EHT.BLL.DTOs;

namespace EHT.BLL.Services.Concrete.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        public Task<ServiceResult> CreateOrUpdateAsync(DepartmentDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> DeleteAsync(int departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDto> GetByIdAsync(int departmentId)
        {
            throw new NotImplementedException();
        }
    }
}
