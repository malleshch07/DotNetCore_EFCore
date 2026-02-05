using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore_EFCore_CQRS.Model
    {
        [Table("tblEmployee")]
        public class Employee
        {
            public int Id { get; set; }
        public string EName { get; set; }
        public string EAddress { get; set; }
        public bool? IsActive { get; set; }
        public decimal Salary { get; set; }
        public long? Mobile { get; set; }
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
