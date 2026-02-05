using DotNetCore_EFCore_CQRS.Model;

namespace DotNetCore_EFCore_CQRS.DTO
{
    public class EmployeeDto
    {
        public string EName { get; set; }
        public string EAddress { get; set; }
        public bool IsActive { get; set; }
        public decimal Salary { get; set; }
        public long Mobile { get; set; }
        public int? DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
