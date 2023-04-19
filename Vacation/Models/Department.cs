namespace Vacation.Models;

public class Department : BaseEntity
{

    public string ShortName { get; set; }
    public string FullName { get; set; }
    public string Note { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
}
