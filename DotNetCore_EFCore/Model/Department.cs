namespace DotNetCore_EFCore_CQRS.Model
{
    public class Department
    {

        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
