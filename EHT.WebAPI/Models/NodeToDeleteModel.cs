using System.ComponentModel.DataAnnotations;

namespace EHT.WebAPI.Models
{
    public class NodeToDeleteModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
