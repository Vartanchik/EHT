using System.Collections.Generic;

namespace EHT.DAL.Entities
{
    public class Organization : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public OrganizationType OrganizationType { get; set; }
        public string Owner { get; set; }
        public ICollection<Country> Countries { get; set; }
    }
}
