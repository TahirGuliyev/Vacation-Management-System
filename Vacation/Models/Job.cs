namespace Vacation.Models;

public class Job : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<VacationDays> VacationDays { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
}
