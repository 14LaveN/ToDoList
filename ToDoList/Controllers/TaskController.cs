using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ToDoList.DAL;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Domain.ViewModels.Task;
using ToDoList.Service.Interfaces;

namespace ToDoList.Controllers;

public class TaskController : Controller
{
    private readonly ITaskService taskService;
    private readonly FirstAppDbContext firstAppDbContext;

    public TaskController(ITaskService taskService,
        FirstAppDbContext firstAppDbContext)
    {
        this.taskService = taskService;
        this.firstAppDbContext = firstAppDbContext;
    }

    public IActionResult TaskForm()
    {
        if (!(firstAppDbContext.Tasks.IsNullOrEmpty()))
        {
            var responseTasksByName = taskService.GetAll();
            return View(responseTasksByName.Data.Where(x => x.Author == User.Identity.Name));
        }
        return View(taskService.GetAll().Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskViewModel model)
    {
        var response = await taskService.Create(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { description = response.Description });
        }
        return BadRequest(new { description = response.Description });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await taskService.Delete(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("TaskForm", "Task");
        }
        return RedirectToAction("TaskForm", "Task");
    }

    [HttpPost]
    public async Task<IActionResult> Update(long id)
    {
        var response = await taskService.Update(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("TaskForm", "Task");
        }
        return RedirectToAction("TaskForm", "Task");
    }

    [HttpPost]
    public async Task<IActionResult> EndTask(long id)
    {
        var response = await taskService.EndTask(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("TaskForm", "Task");
        }
        return RedirectToAction("TaskForm", "Task");
    }
}