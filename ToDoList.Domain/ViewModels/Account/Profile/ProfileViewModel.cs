using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.ViewModels.Account.Profile;

public class ProfileViewModel
{
    
    public required string Name { get; set; }

    public int TasksCount { get; set; }
}
