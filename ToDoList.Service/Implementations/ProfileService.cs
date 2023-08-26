using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Response;
using ToDoList.Domain.ViewModels.Account.Profile;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implementations;

public class ProfileService : IProfileService
{
    private readonly IBaseRepository<ProfileEntity> profileRepository;
    private readonly ILogger<ProfileService> logger;

    public ProfileService(IBaseRepository<ProfileEntity> profileRepository,
        ILogger<ProfileService> logger)
    {
        this.logger = logger;
        this.profileRepository = profileRepository;
    }
    public async Task<IBaseResponse<ProfileEntity>> CreateProfile(CreateProfileViewModel createProfileViewModel)
    {
        try
        {
            logger.LogInformation($"Request for create a profile - {createProfileViewModel.Name}");

            var profile = await profileRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Name == createProfileViewModel.Name);
            if (profile != null)
            {
                return new BaseResponse<ProfileEntity>()
                {
                    Description = "Profile with the same name not found",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            profile = new ProfileEntity()
            {
                Name = createProfileViewModel.Name,
                IsAdmin = false,
                TasksCount = 0
            };
            await profileRepository.Create(profile);

            logger.LogInformation($"Profile created: {profile.Name}");
            return new BaseResponse<ProfileEntity>()
            {
                Description = "Profile created",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[ProfileService.CreateProfile]: {exception.Message}");
            return new BaseResponse<ProfileEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
