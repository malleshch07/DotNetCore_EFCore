using AutoMapper;
using AutoMapper.Execution;
using DotNetCore_EFCore_CQRS.DTO;
using DotNetCore_EFCore_CQRS.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DotNetCore_EFCore_CQRS.Repositories
{
    public class EmployeeQueryRepositoriesService:IEmployeeQueryRepositoriesService
    {

        private readonly AppDBContext _context;
        private readonly IMapper _mapper;
        public EmployeeQueryRepositoriesService(AppDBContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDto>>  GetEmployeeDataBylinq()
        {

            var emp = await _context.employee.Where(x => x.EName.Contains("m")).ToListAsync();



            // approach 1 2 lines one for qeury and 2nd for run
            var emp1 =  from e in _context.employee
                             join
                             d in _context.department on e.DepartmentId equals d.Id select new
                             {
                                 e.Id,
                                 e.EName,
                                 d.DepartmentName,
                                 e.DepartmentId
                             };
            await emp1.ToListAsync();




            // approach 2  1 line  for qeury and  run again mapping need to return

            var emp12 = await ( from e in _context.employee
                       join
                       d in _context.department on e.DepartmentId equals d.Id
                       select new
                       {
                           e.Id,
                           e.EName,
                           d.DepartmentName,
                           e.DepartmentId


                       }).ToListAsync();


            // 3rd way mapping and query and run all in one line
             _mapper.Map<List<EmployeeDto>>(await (from e in _context.employee
                                                         join
                                                         d in _context.department on e.DepartmentId equals d.Id
                                                         select new
                                                         {
                                                             e.Id,
                                                             e.EName,
                                                             d.DepartmentName,
                                                             e.DepartmentId


                                                         }).ToListAsync());



            return _mapper.Map<List<EmployeeDto>>(emp12);

        }


    }
}
