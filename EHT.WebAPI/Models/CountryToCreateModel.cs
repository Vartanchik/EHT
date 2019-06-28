using System.ComponentModel.DataAnnotations;

namespace EHT.WebAPI.Models
{
    public class CountryToCreateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int OrganizationId { get; set; }
    }
}
