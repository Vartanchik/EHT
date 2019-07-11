using System;
using System.Threading.Tasks;
using AutoMapper;
using EHT.BLL.DTOs;
using EHT.DAL.Entities.AppUser;
using EHT.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
    }
}
