using Microsoft.AspNetCore.Mvc;
using ToDoList.DAL;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Domain.ViewModels.Account.Profile;
using ToDoList.Service.Implementations;
using ToDoList.Service.Interfaces;

namespace ToDoList.Controllers;

public class ProfileController : Controller
{
    private readonly IProfileService profileService;
    private readonly IBaseRepository<UserEntity> userRepository;
    private readonly IBaseRepository<TaskEntity> taskRepository;

    public ProfileController(IProfileService profileService,
        IBaseRepository<UserEntity> userRepository,
        IBaseRepository<TaskEntity> taskRepository)
    {
        this.profileService = profileService;
        this.userRepository = userRepository;
        this.taskRepository = taskRepository;
    }

    public async Task<IActionResult> ProfileForm()
    {
        var user = await userRepository.GetByName(User.Identity.Name);
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile(CreateProfileViewModel createProfileViewModel)
    {
        var response = await profileService.CreateProfile(createProfileViewModel);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            var user = await userRepository.GetByName(User.Identity.Name);
            user.IsProfileCreated = true;
            await userRepository.Update(user);
            var task = new TaskEntity
            {
                Author = User.Identity.Name,
                Name = "Start task",
                Description = "delete this task",
                IsDone = false,
                Priority = Priority.Easy,
                CreationDate = DateTime.Now
            };
            await taskRepository.Create(task);
            return Ok(new { description = response.Description });
        }
        return BadRequest(new { description = response.Description });
    }
}
