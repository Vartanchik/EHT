using EHT.BLL.DTOs;
using System.ComponentModel.DataAnnotations;

namespace EHT.WebAPI.Models
{
    public class NodeToCreateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        public NodePropertiesDto Properties { get; set; }
        [Required]
        public int ParentId { get; set; }
    }
}
