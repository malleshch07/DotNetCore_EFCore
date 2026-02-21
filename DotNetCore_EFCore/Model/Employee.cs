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
        //made virtual for lazy loading else not needed
        public virtual Department Department { get; set; }
        public int? DesignationId { get; set; }
        //made virtual for lazy loading else not needed
        public virtual Designation Designation { get; set; }
    }
}
