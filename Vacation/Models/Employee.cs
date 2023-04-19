using Microsoft.AspNetCore.Identity;

namespace Vacation.Models;

public class Employee : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int DepartmentID { get; set; }
    public int JobID { get; set; }
    public string AccountId { get; set; }
    public Account Account { get; set; }

    public virtual Job Job { get; set; }
    public virtual Department Department { get; set; }
    public virtual ICollection<VacationRequest> VacationRequests { get; set; }
}
