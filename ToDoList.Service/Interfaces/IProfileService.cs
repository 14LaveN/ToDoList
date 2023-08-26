using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Response;
using ToDoList.Domain.ViewModels.Account.Profile;

namespace ToDoList.Service.Interfaces;

public interface IProfileService
{
    Task<IBaseResponse<ProfileEntity>> CreateProfile(CreateProfileViewModel createProfileViewModel);
}
