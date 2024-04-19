public class Employee
{

    public int EmpID { get; set; }
    public string EmpName { get; set; }
    public int SalaryPerHour { get; set; }
    public int NoOfWorkingHours { get; set; }
    public int NetSalary => SalaryPerHour * NoOfWorkingHours;
    public static string OrganizationName = "ACCION TECH";
    public readonly string DepartmentName = "Finance Department";
    public const string TypeOfEmployee = "Contract Based";

    
}
