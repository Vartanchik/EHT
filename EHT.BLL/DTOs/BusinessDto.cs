using System.Collections.Generic;

namespace EHT.BLL.DTOs
{
    public class BusinessDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CountryDto Country { get; set; }
    }
}
