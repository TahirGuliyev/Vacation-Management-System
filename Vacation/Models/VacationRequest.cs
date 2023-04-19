namespace Vacation.Models;

public class VacationRequest : BaseEntity
{
    public string Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public virtual Employee Employee { get; set; }
    public int EmployeeID { get; set; }
}
