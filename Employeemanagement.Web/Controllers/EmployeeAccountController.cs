using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Employeemanagement.Web.Models;
using System.Net.Mail;
using Employeemanagement.Web.Interfaces;
using Employeemanagement.Web.DataLayer;
using EmployeeManagement.Models;

namespace Employeemanagement.Web.Controllers;

public class EmployeeAccountController : Controller
{
    private readonly IEmailSender _emailSender;
    private readonly UserManager<EmployeeViewModel> _userManager;
    private readonly EmployeeManagerDbContext _dbContext;
    public EmployeeAccountController(IEmailSender emailSender, UserManager<EmployeeViewModel> userManager)
    {
        _emailSender = emailSender;
        _userManager = userManager;
       // _dbContext = _dbContext
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationViewModel model)
    {
        if (ModelState.IsValid)
        {
            var employee = new EmployeeViewModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                IsEmailConfirmed = false,
                VerificationToken = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(employee);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(employee);
                var confirmationLink = Url.Action("ConfirmEmail", "Account",
                    new { userId = employee.Id, token }, Request.Scheme);

                // Send confirmation email
                await _emailSender.SendEmailAsync(model.Email, "Confirm your email", confirmationLink);

                return RedirectToAction("SuccessRegistration");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }
    [HttpGet("sendemailverification")]
    public async Task<IActionResult> SendVerificationEmail(string email)
    {
        var subject = "Email Verification";
        var message = "Please verify your email by clicking the link.";
        await _emailSender.SendEmailAsync(email, subject, message);

        return Ok("Verification email sent.");
    }

    [HttpGet("emailconfirmation")]
    public async Task<IActionResult> ConfirmEmail(string employeeId, string token)
    {
        if (employeeId == null || token == null)
        {
            return RedirectToAction("Error", "Home");
        }
        var employee = await _userManager.FindByEmailAsync(employeeId);
        if (employee is null)
            return RedirectToAction("Error", "Home");

        var result = await _userManager.ConfirmEmailAsync(employee, token);
        if (result.Succeeded)
        {
            return RedirectToAction("EmailConfirmed", "Account");
        }

        return RedirectToAction("Error", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> InviteTeamMember(InviteTeamMemberViewModel model)
    {
        var employee = await _userManager.GetUserAsync(EmployeeViewModel);
        if (employee == null)
        {
            return RedirectToAction("Error", "Home");
        }

        var newEmployee = new EmployeeViewModel
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            CompanyName = employee.CompanyName, // Automatically assign to same company
            EmailConfirmed = true // Directly confirm email
        };

        var result = await _userManager.CreateAsync(newEmployee);
        if (result.Succeeded)
        {
            return RedirectToAction("TeamManagement");
        }

        return View(model);
    }
}