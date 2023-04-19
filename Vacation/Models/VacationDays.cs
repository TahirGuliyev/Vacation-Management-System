namespace Vacation.Models;

public class VacationDays : BaseEntity
{
    public int DaysCount { get; set; }
    public string Note { get; set; }

    public virtual Job Job { get; set; }
    public int JobID { get; set; }
}
