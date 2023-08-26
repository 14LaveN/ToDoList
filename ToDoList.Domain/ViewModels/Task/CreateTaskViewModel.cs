using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.Enum;

namespace ToDoList.Domain.ViewModels.Task;

public class CreateTaskViewModel
{
    [Range(2, 23, ErrorMessage = "Enter a task name")]
    public required string Name { get; set; }

    [Range(1, 230, ErrorMessage = "Enter a description")]
    public required string Description { get; set; }
    
    public required string Author { get; set; }

    public Priority Priority { get; set; }
}