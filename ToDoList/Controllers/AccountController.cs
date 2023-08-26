using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoList.Domain.ViewModels.Account.Register;
using ToDoList.Service.Implementations;
using ToDoList.Service.Interfaces;
using ToDoList.Domain.ViewModels.Account.Login;

namespace ToDoList.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    public IActionResult RegisterForm()
    {
        return View();
    }

    [HttpGet]
    public IActionResult LoginForm()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        var response = await accountService.Register(registerViewModel);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response.Data));

            return Ok(new { description = response.Description });
        }

        return BadRequest(new { description = response.Description });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var response = await accountService.Login(loginViewModel);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response.Data));

            return RedirectToAction("TaskForm", "Task");
        }

        return View(loginViewModel);
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("TaskForm", "Task");
    }
}
