using DotNetCore_EFCore_CQRS.DTO;

namespace DotNetCore_EFCore_CQRS.Commands
{
    public interface IEmployeeQuery
    {
        Task<List<EmployeeDto>> GetEmployeeDataBylinq();
    }
}
