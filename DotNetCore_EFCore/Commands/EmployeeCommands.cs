using DotNetCore_EFCore_CQRS.DTO;
using DotNetCore_EFCore_CQRS.Model;
using DotNetCore_EFCore_CQRS.Repositories;
using DotNetCore_EFCore_CQRS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore_EFCore_CQRS.Commands
{
    public class EmployeeCommands : IEmployeeCommands
    {

        private readonly IEmployeeCommandRepositoriesService _IEmpCommandRepo;
        private readonly AppDBContext _Context;


        public EmployeeCommands(IEmployeeCommandRepositoriesService IEmpCmdRepo, AppDBContext context)
        {
            _Context = context;
            _IEmpCommandRepo = IEmpCmdRepo;
        }
        public async Task<int> SaveEmployee(EmployeeDto DTO)
        {


            var employee = new Employee
            {
                EName = DTO.EName,
                EAddress = DTO.EAddress,
                IsActive = DTO.IsActive,
                Salary = DTO.Salary,
                Mobile = DTO.Mobile
            };

            _Context.Entry(employee).Property("CreatedBy").CurrentValue = "API";
            _Context.Entry(employee).Property("UpdatedDate").CurrentValue = DateTime.UtcNow;

            _Context.Entry(employee).Property("IsDeleted").CurrentValue = false;

            if (_IEmpCommandRepo.CheckByName(DTO.EName))
            {

                throw new Exception("user exist already");
            }
        await    _IEmpCommandRepo.SaveEmployee(employee);
            return employee.Id;

        }

        public Task<List<EmployeeDto>> GetEmployee()
        {

            return _IEmpCommandRepo.GetEmployee();
        }

        public async Task<List<EmployeeDto>> GetEmployeewithDepartment(int? deptId)
        {

           return await _IEmpCommandRepo.GetEmployeewithDepartment(deptId);



        }


    }
}
