using DotNetCore_EFCore_CQRS.DTO;
using DotNetCore_EFCore_CQRS.Model;

namespace DotNetCore_EFCore_CQRS.Commands
{
    public interface IEmployeeCommands
    {
        Task<int> SaveEmployee(EmployeeDto emp);
        Task<List<EmployeeDto>> GetEmployee();
        Task<List<EmployeeDto>> GetEmployeewithDepartment(int? deptId);
        //Task<List<EmployeeDto>> GetEmployeewithDepartwithEFlazyload();
        Task<List<EmployeeDto>> GetEmployeewithDepartwithEFExplictLoading();

    }
}
