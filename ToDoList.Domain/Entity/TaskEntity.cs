﻿using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Enum;

namespace ToDoList.Domain.Entity;

[Index(nameof(Id), IsUnique = true)]
public class TaskEntity
{
    public long Id { get; set; }

    public string Author { get; set; }
    
    public string Name { get; set; }

    public bool IsUpdateNow { get; set; }
    
    public bool IsDone { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public Priority Priority { get; set; }
}