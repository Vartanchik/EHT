using EHT.DAL.Entities.AppUser;
using EHT.DAL.Repositories.GenericRepository;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EHT.DAL.Repositories.ConcreteRepositories.AppUserRepository
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository<AppUser>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AppUserRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
            : base(context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityResult> CreateAsync(AppUser appUser, string password)
        {
            return await _userManager.CreateAsync(appUser, password);
        }

        public async Task<bool> CheckPasswordAsync(int userId, string password)
        {
            var appUser = await _userManager.FindByIdAsync(userId.ToString());

            return await _userManager.CheckPasswordAsync(appUser, password);
        }

    }
}
