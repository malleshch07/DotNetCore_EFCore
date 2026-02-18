using DotNetCore_EFCore_CQRS.DTO;

namespace DotNetCore_EFCore_CQRS.Repositories
{
    public interface IEmployeeQueryRepositoriesService
    {
        Task<List<EmployeeDto>> GetEmployeeDataBylinq();
    }
}
