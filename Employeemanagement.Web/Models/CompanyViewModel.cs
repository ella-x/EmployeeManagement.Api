using System.ComponentModel.DataAnnotations;

namespace Employeemanagement.Web.Models;

public class CompanyViewModel
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string VatNumber { get; set; }
    public string UserEmail { get; set; }
}