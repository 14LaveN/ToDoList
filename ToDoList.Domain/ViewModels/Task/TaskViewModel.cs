using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Enum;

namespace ToDoList.Domain.ViewModels.Task;

public class TaskViewModel
{
    [Range(2, 23, ErrorMessage = "Enter a task name")]
    public required string Name { get; set; }

    [Range(1, 230, ErrorMessage = "Enter a description")]
    public required string Description { get; set; }

    public required Priority Priority { get; set; }
}