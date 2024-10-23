using System.ComponentModel.DataAnnotations;

namespace Employeemanagement.Web.Models;

public class InviteTeamMemberViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public string VerificationToken { get; set; }
}