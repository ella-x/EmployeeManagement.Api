using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace Employeemanagement.Web.Models;

public class EmployeeViewModel : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CompanyName { get; set; }
    public string Email { get; set; }
    public string VATNumber { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public string VerificationToken { get; set; }
    public virtual Company Company { get; set; }
}