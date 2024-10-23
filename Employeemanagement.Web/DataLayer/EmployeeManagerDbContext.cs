using Employeemanagement.Web.Models;
using EmployeeManagement.Api.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Employeemanagement.Web.DataLayer;

public class EmployeeManagerDbContext : IdentityDbContext
{
    public EmployeeManagerDbContext(DbContextOptions<EmployeeManagerDbContext> options) : base(options) { }

    public DbSet<CompanyViewModel> Companies { get; set; }
    public DbSet<EmployeeViewModel> Employees { get; set; }
    public DbSet<InviteTeamMemberViewModel> TeamMembers { get; set; }
}