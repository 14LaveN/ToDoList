using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Entity;

public class ProfileEntity
{
    public long Id { get; set; }

    public string Name { get; set; }

    public int TasksCount { get; set; }

    public bool IsAdmin { get; set; }
}