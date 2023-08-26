using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Response;
using ToDoList.Domain.ViewModels.Account.Login;
using ToDoList.Domain.ViewModels.Account.Register;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implementations;

public class AccountService : IAccountService
{
    private readonly ILogger<AccountService> logger;
    private readonly IBaseRepository<UserEntity> userRepository;

    public AccountService(ILogger<AccountService> logger,
        IBaseRepository<UserEntity> userRepository)
    {
        this.logger = logger;
        this.userRepository = userRepository;
    }

    public async Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel registerViewModel)
    {
        try
        {
            logger.LogInformation($"Request for register a user: {registerViewModel.Name}");
            var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == registerViewModel.Name);

            if (user != null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "User with the same login already taken",
                };
            }

            user = new UserEntity()
            {
                Name = registerViewModel.Name,
                Email = registerViewModel.Email,
                IsProfileCreated = false,
                Role = Role.User,
                Password = HashPasswordHelper.HashPassowrd(registerViewModel.Password)
            };

            await userRepository.Create(user);

            logger.LogInformation($"User created: {user.Name}");

            var result = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "User created",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[AcountService.Register]: {exception.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel loginViewModel)
    {
        try
        {
            logger.LogInformation($"Request for login a user: {loginViewModel.Name}");
            var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == loginViewModel.Name);
            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "User not found"
                };
            }

            if (user.Password != HashPasswordHelper.HashPassowrd(loginViewModel.Password))
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Password or login not found"
                };
            }

            logger.LogInformation($"User logined: {user.Name}");

            var result = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[AccountService.Login]: {exception.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    private ClaimsIdentity Authenticate(UserEntity userEntity)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, userEntity.Name),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, userEntity.Role.ToString())
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}