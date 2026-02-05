using AutoMapper;
using DotNetCore_EFCore_CQRS.DTO;
using DotNetCore_EFCore_CQRS.Model;
using DotNetCore_EFCore_CQRS.Services;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore_EFCore_CQRS.Repositories
{
    public class EmployeeCommandRepositoriesService : IEmployeeCommandRepositoriesService
    {
        private readonly AppDBContext _Context;

        private readonly IMapper _mapper;
        public EmployeeCommandRepositoriesService(AppDBContext context,IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }
        public async Task SaveEmployee(Employee empDetails)
        {
            //await with async must use task not void . 
            //        await Task.Delay(5000);
            //throw new Exception("DB failed");
            _Context.employee.Add(empDetails);
            await _Context.SaveChangesAsync();

        }

        public bool CheckByName(string strName)
        {
            return _Context.employee.Any(x => x.EName == strName);

        }

        public async Task<List<EmployeeDto>> GetEmployee()
        {

            //return  await _Context.employee.Where(x => x.IsActive == true).ToListAsync();


            //return await _Context.employee.Where(x => x.IsActive == true).Select(e => new EmployeeDto
            //{

            //    EName = e.EName,
            //    Salary = e.Salary
            //        ,
            //    DepartmentId = e.DepartmentId
            //        ,
            //    DepartmentName = e.Department != null ? e.Department.DepartmentName : null

            //}).ToListAsync();

            var emp = await _Context.employee.Where(x => x.IsActive == true).AsNoTracking().ToListAsync();

            return _mapper.Map<List<EmployeeDto>>(emp);



        }
    }
}
