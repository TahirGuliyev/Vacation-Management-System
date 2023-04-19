using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vacation.Models;

public class Account : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? EmployeeID { get; set; }

    [ForeignKey("EmployeeID")]
    public Employee Employee { get; set; }

    public string UserRole { get; set; } = "";
    public override string SecurityStamp { get; set; } = System.Guid.NewGuid().ToString();
}
