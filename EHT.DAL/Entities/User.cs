using Microsoft.AspNetCore.Identity;

namespace EHT.DAL.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        public string Surname { get; set; }
        //public string Email { get; set; }
        public string Addres { get; set; }
    }
}
