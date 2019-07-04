using EHT.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHT.BLL.Services.Concrete.TreeService
{
    public interface ITreeService
    {
        Task<IList<NodeDto>> GetTreeAsync();
        Task<ServiceResult> CreateNodeAsync(NodeDto dto);
        Task<ServiceResult> UpdateNodeAsync(NodeDto dto);
        Task<ServiceResult> DeleteNodeAsync(NodeDto dto);
    }
}
