using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Api.Data;
using Employeemanagement.Web.Models;
using Employeemanagement.Web.Interfaces;
using Employeemanagement.Web.Services;
using Microsoft.AspNetCore.Identity;
using Employeemanagement.Web.DataLayer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultIdentity<EmployeeViewModel>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<EmployeeManagerDbContext>();
builder.Services.AddIdentity<EmployeeViewModel, IdentityRole>()
    .AddEntityFrameworkStores<EmployeeManagerDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddTransient<IEmailSender, EmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EmployeeManagerDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
