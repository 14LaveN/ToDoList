using ToDoList.Domain.Entity;
using ToDoList.Domain.Response;
using ToDoList.Domain.ViewModels.Task;

namespace ToDoList.Service.Interfaces;

public interface ITaskService
{
    Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel model);

    Task<IBaseResponse<TaskEntity>> Delete(long id);

    Task<IBaseResponse<TaskEntity>> Update(CreateTaskViewModel createTaskViewModel);

    IBaseResponse<IQueryable<TaskEntity>> GetAll();

    Task<IBaseResponse<TaskEntity>> EndTask(long id);
}