using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.ViewModels.Account.Register;

public class RegisterViewModel
{
    [Range(2, 23, ErrorMessage = "Enter a name")]
    public required string Name { get; set; }

    [Range(6, 24, ErrorMessage = "Enter a password")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords not equals")]
    public required string PasswordConfirm { get; set; }

    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
}
