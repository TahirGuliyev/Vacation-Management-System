using Microsoft.AspNetCore.Identity;

namespace Vacation.Models
{
    public class Role : IdentityRole<int>
    {
        public const string HR = "HR";
        public const string Employee = "Employee";
    }
}
