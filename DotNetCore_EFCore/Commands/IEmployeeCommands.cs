using DotNetCore_EFCore_CQRS.DTO;
using DotNetCore_EFCore_CQRS.Model;

namespace DotNetCore_EFCore_CQRS.Commands
{
    public interface IEmployeeCommands
    {
        int SaveEmployee(EmployeeDto emp);
        Task<List<EmployeeDto>> GetEmployee();

    }
}
