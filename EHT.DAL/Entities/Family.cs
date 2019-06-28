using System.Collections.Generic;

namespace EHT.DAL.Entities
{
    public class Family : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BusinessId { get; set; }
        //public int CountryId { get; set; }
        public Business Business { get; set; }
        public ICollection<Offering> Offerings { get; set; }
    }
}
