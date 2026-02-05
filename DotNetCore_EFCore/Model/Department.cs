namespace DotNetCore_EFCore_CQRS.Model
{
    public class Department
    {

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
