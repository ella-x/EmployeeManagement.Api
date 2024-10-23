using Employeemanagement.Web.DataLayer;
using Employeemanagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employeemanagement.Web.Controllers;

public class CompanyController : Controller
{
    private readonly EmployeeManagerDbContext _dbContext;

    public CompanyController(EmployeeManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> AddCompanyDetails(CompanyViewModel company)
    {
        if (ModelState.IsValid)
        {
            // Save company details
            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync();

            return View("CompanyDetailsSubmitted");
        }

        return View("CompanyDetails", company);
    }
}