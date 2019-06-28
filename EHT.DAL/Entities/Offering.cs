using System.Collections.Generic;

namespace EHT.DAL.Entities
{
    public class Offering : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FamilyId { get; set; }
        //public int BusinessId { get; set; }
        public Family Family { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
