namespace EHT.DAL.Entities
{
    public class Department : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OfferingId { get; set; }
        public Offering Offering { get; set; }
    }
}
