using System.Security.Claims;
using ToDoList.Domain.Response;
using ToDoList.Domain.ViewModels.Account.Login;
using ToDoList.Domain.ViewModels.Account.Register;

namespace ToDoList.Service.Interfaces;

public interface IAccountService
{
    Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel registerViewModel);

    Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel loginViewModel);
}
