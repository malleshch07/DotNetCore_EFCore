using DotNetCore_EFCore_CQRS.Commands;
using DotNetCore_EFCore_CQRS.DTO;
using DotNetCore_EFCore_CQRS.Model;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_EFCore_CQRS.Repositories
{
    public interface IEmployeeCommandRepositoriesService
    {

        Task SaveEmployee(Employee emp);

        bool CheckByName(string strEName);
        Task<List<EmployeeDto>> GetEmployee();

    }

}
