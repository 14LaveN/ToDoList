using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Response;
using ToDoList.Domain.ViewModels.Task;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implementations;

public class TaskService : ITaskService
{
    private readonly IBaseRepository<TaskEntity> taskRepository;
    private ILogger<TaskService> logger;

    public TaskService(IBaseRepository<TaskEntity> taskRepository,
        ILogger<TaskService> logger)
    {
        this.taskRepository = taskRepository;
        this.logger = logger;
    }

    public async Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel createTaskViewModel)
    {
        try
        {
            logger.LogInformation($"Request for create a task - {createTaskViewModel.Name}");

            var task = await taskRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Name == createTaskViewModel.Name);
            if (task != null)
            {
                return new BaseResponse<TaskEntity>()
                {
                    Description = "Task with the same name not found",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            task = new TaskEntity()
            {
                Name = createTaskViewModel.Name,
                Description = createTaskViewModel.Description,
                Author = createTaskViewModel.Author,
                IsDone = false,
                Priority = createTaskViewModel.Priority,
                CreationDate = DateTime.Now
            };
            await taskRepository.Create(task);

            logger.LogInformation($"Task created: {task.Name} {task.CreationDate}");
            return new BaseResponse<TaskEntity>()
            {
                Description = "Task created",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[TaskService.Create]: {exception.Message}");
            return new BaseResponse<TaskEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<TaskEntity>> Delete(long id)
    {
        try
        {
            var task = await taskRepository.GetById(id);

            logger.LogInformation($"Request for delete a task - {task.Name}");

            if (task == null)
            {
                return new BaseResponse<TaskEntity>()
                {
                    Description = "Task with the same name not found",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }
            await taskRepository.Delete(task);

            logger.LogInformation($"Task deleted: {task.Name} {task.CreationDate}");
            return new BaseResponse<TaskEntity>()
            {
                Description = "Task deleted",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[TaskService.Delete]: {exception.Message}");
            return new BaseResponse<TaskEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<TaskEntity>> Update(long id)
    {
        try
        {
            var task = await taskRepository.GetById(id);

            logger.LogInformation($"Request for update a task - {task.Name}");

            if (task == null)
            {
                return new BaseResponse<TaskEntity>()
                {
                    Description = "Task with the same name not found",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }
            await taskRepository.Update(task);

            logger.LogInformation($"Task updated: {task.Name} {task.CreationDate}");
            return new BaseResponse<TaskEntity>()
            {
                Data = task,
                Description = "Task updated",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[TaskService.Update]: {exception.Message}");
            return new BaseResponse<TaskEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public IBaseResponse<IQueryable<TaskEntity>> GetAll()
    {
        try
        {
            var tasks = taskRepository.GetAll();

            logger.LogInformation("Request for get all tasks");

            if (tasks.IsNullOrEmpty())
            {
                return new BaseResponse<IQueryable<TaskEntity>>()
                {
                    Description = "Task with the same name not found",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            logger.LogInformation($"Get tasks: {DateTime.Now}");

            return new BaseResponse<IQueryable<TaskEntity>>()
            {
                Data = tasks,
                Description = "Get tasks",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[TaskService.GetAll]: {exception.Message}");
            return new BaseResponse<IQueryable<TaskEntity>>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<TaskEntity>> EndTask(long id)
    {
        try
        {
            var task = await taskRepository.GetById(id);

            logger.LogInformation($"Request for end a task - {task.Name}");

            if (task == null)
            {
                return new BaseResponse<TaskEntity>()
                {
                    Description = "Task with the same name not found",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }
            task.IsDone = true;
            await taskRepository.Update(task);

            logger.LogInformation($"End task: {task.Name} {task.CreationDate}");
            return new BaseResponse<TaskEntity>()
            {
                Data = task,
                Description = "End task",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[TaskService.Update]: {exception.Message}");
            return new BaseResponse<TaskEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}