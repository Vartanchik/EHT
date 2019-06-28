using System.Collections.Generic;

namespace EHT.DAL.Entities
{
    public class Business : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        //public int OrganizationId { get; set; }
        public Country Country { get; set; }
        public ICollection<Family> Families { get; set; }
    }
}
