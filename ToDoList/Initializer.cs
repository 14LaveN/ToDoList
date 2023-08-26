using Microsoft.AspNetCore.Cors.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;
using ToDoList.Domain.Entity;
using ToDoList.Service.Interfaces;
using ToDoList.Service.Implementations;

namespace ToDoList;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<TaskEntity>, TaskRepository>();
        services.AddScoped<IBaseRepository<UserEntity>, UserRepository>();
        services.AddScoped<IBaseRepository<ProfileEntity>, ProfileRepository>();
    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IProfileService, ProfileService>();
    }
}
