using System.ComponentModel.DataAnnotations;

namespace UniversityCafeteria.Models;

public class RegisterViewModel
{
    [Required, StringLength(100)]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = "";

    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required, DataType(DataType.Password), MinLength(6)]
    public string Password { get; set; } = "";

    [Required, DataType(DataType.Password), Compare(nameof(Password))]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; } = "";
}

public class LoginViewModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}

public class CreateSupplyRequestViewModel
{
    public List<RequestLineViewModel> Lines { get; set; } = new();
}

public class RequestLineViewModel
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class UserRoleViewModel
{
    public string UserId { get; set; } = "";
    public string Email { get; set; } = "";
    public string CurrentRole { get; set; } = "User";

    [Required]
    public string NewRole { get; set; } = "User";
}
