using System.Collections.Generic;

namespace EHT.BLL.DTOs
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public OrganizationDto Organization { get; set; }
    }
}
