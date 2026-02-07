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
        public async Task<int> SaveEmployee(Employee empDetails)
        {
            //await with async must use task not void . 
            //        await Task.Delay(5000);
            //throw new Exception("DB failed");
            _Context.employee.Add(empDetails);
            await _Context.SaveChangesAsync();
            return empDetails.Id;
        }

        public bool CheckByName(string strName)
        {
            return _Context.employee.Any(x => x.EName == strName);

        }

        public async Task<List<EmployeeDto>> GetEmployee()
        {
            //easy way
            //return  await _Context.employee.Where(x => x.IsActive == true).ToListAsync();

            int page = 1;
            int size = 10;

            //no mapper  way
            //return await _Context.employee.Where(x => x.IsActive == true).Select(e => new EmployeeDto
            //{

            //    EName = e.EName,
            //    Salary = e.Salary
            //        ,
            //    DepartmentId = e.DepartmentId
            //        ,
            //    DepartmentName = e.Department != null ? e.Department.DepartmentName : null

            //}).ToListAsync();


            // mapper  way
            var emp = await _Context.employee.Where(x => x.IsActive == true).OrderBy(e => e.Id).Skip((page-1)*size).Take(size).AsNoTracking().ToListAsync();
            return _mapper.Map<List<EmployeeDto>>(emp);


            // iqueryable

            //var query = _Context.employee.AsQueryable();

            //query = query.Where(x => x.IsActive == true);
            //query = query.Where(e => e.EName.Contains("mallesh"));


            //var emp1 = await query.AsNoTracking().ToListAsync();

            //return _mapper.Map<List<EmployeeDto>>(emp1);

        }

        public async Task<List<EmployeeDto>> GetEmployeewithDepartment(int? deptId)
        {
            // only loads employee columns as DepartmentId also present so it loades departmentid but not Departmentdata of the each employee
            var employeonly= await _Context.employee.ToListAsync();
            //  loads employee columns and department table  loades  Departmentdata of the each employee as we added include.

            var employewithDepart = await _Context.employee.Include(e => e.Department).ToListAsync();



            // only loads employee columns as DepartmentId also present so it loades departmentid but not Departmentdata of the each employee with selccting 
            // few fileds as select is simlar to DB select


            var emponlyselect= await _Context.employee.Select(e => new EmployeeDto
            {
                EAddress = e.EAddress,
                IsActive = e.IsActive,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department != null ? e.Department.DepartmentName : null,
                EName = e.EName
            }).ToListAsync();

            var empwithDepartselect = await _Context.employee.Include(e=>e.Department).Select(e => new EmployeeDto
            {
                EAddress = e.EAddress,
                IsActive = e.IsActive,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department != null? e.Department.DepartmentName:null,
                EName = e.EName
            }).ToListAsync();



            var empwithWHere = _Context.employee.Where(x => x.IsActive == true).ToListAsync();


            var empwithDepartIncludeWHere = _Context.employee.Where(x => x.IsActive == true).Include(e => e.Department).ToListAsync();
            //above and below same even u added include before where or after where best is above to understand
            var empwithDepartIncludeWHere1 = _Context.employee.Include(e => e.Department).Where(x => x.IsActive == true).ToListAsync();
            // order by
            var EmpwithDepartInclideOrderBy = _Context.employee.Where(x => x.EAddress.Contains("Hi")).Include(e => e.Department).OrderBy(x => x.EName).ToListAsync();
           
            // all options
            
            var Empalloptions = _Context.employee.AsNoTracking()
                .Where(x => x.Salary > 1000 && (x.DepartmentId== deptId || x.Department.DepartmentName=="HR"))
                .Include(x => x.Department)
                .OrderByDescending(x => x.Salary)
                .Skip(1)
                .Take(5).Select(e => new EmployeeDto
                {
                    EAddress = e.EAddress,
                    DepartmentName = e.Department != null ? e.Department.DepartmentName : null,
                    Salary = e.Salary,
                    DepartmentId= e.DepartmentId
                }).ToListAsync();

            var empany = _Context.employee.AnyAsync(x => x.IsActive==true);



            //group by

            var empgroupby = await _Context.employee.Where(x=>x.DepartmentId!= null).GroupBy(f => f.Department.Id).Select(x => new
            {
                depart = x.Key,
                count = x.Count()
            }).ToListAsync();

            foreach (var item in empgroupby)
            {
                Console.WriteLine($"DepartmentId: {item.depart}, Count: {item.count}");
            }
            return empwithDepartselect;




        }
    }
}
