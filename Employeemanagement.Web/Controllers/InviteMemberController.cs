using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Employeemanagement.Web.Interfaces;
using Employeemanagement.Web.DataLayer;
using Employeemanagement.Web.Models;

namespace Employeemanagement.Web.Controllers;

public class InviteMemberController : Controller
{
    private readonly IEmailSender _emailSender;
    private readonly EmployeeManagerDbContext _dbContext;

    public InviteMemberController(IEmailSender emailSender, EmployeeManagerDbContext dbContext)
    {
        _emailSender = emailSender;
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> SendInvitation(InviteTeamMemberViewModel invitation)
    {
        if (ModelState.IsValid)
        {
            // Generate verification token
            invitation.VerificationToken = Guid.NewGuid().ToString();
            invitation.IsEmailConfirmed = false;

            // Save to the database
            _dbContext.TeamMembers.Add(invitation);
            await _dbContext.SaveChangesAsync();

            // Create verification link
            var verificationLink = Url.Action("ConfirmEmail", "Invite", new { token = invitation.VerificationToken }, Request.Scheme);

            // Send email
            await _emailSender.SendEmailAsync(invitation.Email, "Confirm your email", $"Please confirm your email by clicking <a href='{verificationLink}'>here</a>.");

            return View("InvitationSent");
        }

        return View("InviteUser", invitation);
    }

    // Email confirmation action
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string token)
    {
        var userInvitation = _dbContext.TeamMembers.FirstOrDefault(u => u.VerificationToken == token);
        if (userInvitation != null)
        {
            userInvitation.IsEmailConfirmed = true;
            await _dbContext.SaveChangesAsync();
            return View("EmailConfirmed");
        }

        return View("Error");
    }
}