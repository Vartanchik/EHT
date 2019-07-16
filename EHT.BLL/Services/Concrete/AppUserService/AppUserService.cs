using System;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using EHT.BLL.DTOs;
using EHT.DAL.Entities.AppUser;
using EHT.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

namespace EHT.BLL.Services.Concrete.AppUserService
{
    public class AppUserService : IAppUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<AppUserService> _logger;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AppUserService> logger)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult> CreateAsync(AppUserDto dto, string password)
        {
            try
            {
                var appUser = _mapper.Map<AppUser>(dto);

                var appUserExist = await _uow.AppUsers.AsQueryable()
                                                      .AnyAsync(u => u.Email == appUser.Email &&
                                                                     u.UserName == appUser.UserName);

                if (appUserExist) return new ServiceResult($"User with name: {appUser.UserName} or email: {appUser.Email} - already exist.");

                var result = await _uow.AppUsers.CreateAsync(appUser, password);

                return new ServiceResult { Succeeded = result.Succeeded };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.ToString()} - error message:{ex.Message}");

                return new ServiceResult(ex.Message);
            }
        }

        public string GenerateToken(int userId, string securityKey, string expireTime)
        {
            try
            {
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

                var signinCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                var claims = new List<Claim>
                {
                    new Claim("userId", userId.ToString())
                };

                var token = new JwtSecurityToken(
                        expires: DateTime.UtcNow.AddDays(Convert.ToInt32(expireTime)),
                        signingCredentials: signinCredentials,
                        claims: claims
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.ToString()} - error message:{ex.Message}");

                return null;
            }
        }

        public async Task<bool> CheckPasswordAsync(int userId, string password)
        {
            return await _uow.AppUsers.CheckPasswordAsync(userId, password);
        }

        public async Task<int> GetUserIdAsync(string email)
        {
            return await _uow.AppUsers.AsQueryable()
                                        .Where(u => u.Email == email)
                                        .Select(u => u.Id)
                                        .FirstOrDefaultAsync();
        }
    }
}
