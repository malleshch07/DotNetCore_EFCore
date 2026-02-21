namespace DotNetCore_EFCore_CQRS.Model
{
    public class Designation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Grade { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
