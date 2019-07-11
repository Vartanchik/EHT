using Microsoft.AspNetCore.Identity;

namespace EHT.DAL.Entities.AppUser
{
    public class AppUser : IdentityUser<int>, IEntity
    {
        public string Surname { get; set; }
        public string Addres { get; set; }
    }
}
