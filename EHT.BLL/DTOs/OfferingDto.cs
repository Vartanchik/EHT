using System.Collections.Generic;

namespace EHT.BLL.DTOs
{
    public class OfferingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FamilyDto Family { get; set; }
    }
}
