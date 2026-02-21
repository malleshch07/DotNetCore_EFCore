using AutoMapper;
using AutoMapper.Execution;
using Castle.Core.Logging;
using DotNetCore_EFCore_CQRS.DTO;
using DotNetCore_EFCore_CQRS.Model;
using DotNetCore_EFCore_CQRS.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Net;

namespace DotNetCore_EFCore_CQRS.Repositories
{
    public class EmployeeQueryRepositoriesService : IEmployeeQueryRepositoriesService
    {

        private readonly AppDBContext _context;
        private readonly IMapper _mapper;
        public EmployeeQueryRepositoriesService(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDto>> GetEmployeeDataBylinq()
        {

            var emp = await _context.employee.Where(x => x.EName.Contains("m")).ToListAsync();



            // approach 1 2 lines one for qeury and 2nd for run
            var emp1 = from e in _context.employee
                       join
                       d in _context.department on e.DepartmentId equals d.Id
                       select new
                       {
                           e.Id,
                           e.EName,
                           d.DepartmentName,
                           e.DepartmentId
                       };
            await emp1.ToListAsync();




            // approach 2  1 line  for qeury and  run again mapping need to return

            var emp12 = await (from e in _context.employee
                               join
                               d in _context.department on e.DepartmentId equals d.Id
                               select new
                               {
                                   e.Id,
                                   e.EName,
                                   d.DepartmentName,
                                   e.DepartmentId


                               }).ToListAsync();


            //query
            var emp1334 = (from e in _context.employee
                           where e.EName.Length > 0
                           orderby e.EName ascending, e.Mobile descending
                           select new
                           {
                               ename = e.EName,

                           });

            //method
            var eemmee = await _context.employee
                .Where(x => !string.IsNullOrWhiteSpace(x.EName) && x.EName.Length > 2)
                .OrderBy(e => e.EName)
                .ThenByDescending(e => e.Mobile).ThenBy(k => k.EAddress)
                .Select(x => new EmployeeDto
                {
                    EName = x.EName,
                    EAddress = x.EAddress

                })
                .AsNoTracking()
            .ToListAsync();


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

        public async Task<EmployeeDto> GetEmployeeDetailsById(int id)
        {


            var emp = await _context.employee.Where(emp => emp.Id == id).FirstOrDefaultAsync();


            var emp1 = await (from empa in _context.employee
                              where empa.Id.Equals(id)
                              select new EmployeeDto
                              {
                                  EAddress = empa.EAddress
            ,
                                  EName = empa.EName
                              }).FirstOrDefaultAsync();

            //iquerable
            var empq1 = from empa in _context.employee
                        where empa.Id.Equals(id)
                        select new EmployeeDto
                        {
                            EAddress = empa.EAddress
    ,
                            EName = empa.EName

                        };

            //execution
            var emmpp = await empq1.FirstOrDefaultAsync();


            //emp1.FirstOrDefaultAsync();

            // return new object when data null
            //return _mapper.Map<EmployeeDto>(emp1) ?? new EmployeeDto();

            // return new object when data null same but used null checking explict.

            return emp1 != null ? _mapper.Map<EmployeeDto>(emp1) : null;



        }

        public async Task<EmployeeDto> GetEmployeeDetailsBydynamicSort(string sortby)
        {

            IQueryable<Employee> query = _context.employee;

            query = sortby.ToLower() == "salary" ? query.OrderBy(x => x.Salary) : query.OrderBy(x => x.EName);

            Console.WriteLine(query.ToString());
            var result = await query.FirstAsync();


            return _mapper.Map<EmployeeDto>(result);
        }

        public async Task<dynamic> GetEmployeebyPageSize(int pagenumber, int Pagesize)
        {
            // if we dont make select for dynamic return then dependence like departmntid values can cause circle dependence
            // throws error.
            var emp = await _context.employee.OrderBy(x => x.EName).Skip((pagenumber - 1) * Pagesize).Take(Pagesize).
                Select(e => new EmployeeDto
                {
                    EAddress = e.EAddress,
                    EName = e.EName
                ,
                    IsActive = e.IsActive,
                }).ToListAsync();
            return (emp);
        }
        public async Task<dynamic> GetEmployeebyCount_Any_LongCount(string name)
        {
            int countrecords = 0;
            long longcountrecords = 0;
            List<Employee> emp = new List<Employee>();
            if (await _context.employee.AnyAsync(x => x.EName != name))
            {

                countrecords = await _context.employee.Where(x => x.EName != name).CountAsync();
                longcountrecords = await _context.employee.Where(x => x.EName != name).LongCountAsync();

            }
            if (countrecords > 0)
            {
                emp = await _context.employee.Where(x => x.EName != name).ToListAsync();
            }
            return new
            {
                datapresnt = true,
                count = countrecords,
                longcount = longcountrecords,
                empe = emp.Select(x => new EmployeeDto()
                {
                    EAddress = x.EAddress,
                    EName = x.EName
                ,
                    IsActive = x.IsActive,
                }),
            };

        }
        public async Task<dynamic> GetEmployeebyFirstOrSingle(string name)
        {

            // throws error if no record

            if (await _context.employee.AnyAsync(x => x.EName.Contains(name)))
            {
                var first = await _context.employee.Where(x => x.EName.Contains(name)).FirstAsync();
                var first1 = await _context.employee.FirstAsync(x => x.EName.Contains(name));
            }
            //  if no record return null

            var firstorDefault = await _context.employee.FirstOrDefaultAsync(x => x.EName.Contains(name));

            // throws error if no record or 2 reocrds found


            var count = await _context.employee.CountAsync(x => x.EName.Contains(name));

            if (count == 1)
            {
                var single = await _context.employee.SingleAsync(x => x.EName.Contains(name));
            }

            var singleOrDefault = await _context.employee.SingleOrDefaultAsync(x => x.EName.Contains(name));

            var emp = await _context.employee.Where(x => EF.Functions.Like(x.EName, $"%{name}%")).Select(x => new EmployeeDto
            {
                EName = x.EName,
                EAddress = x.EAddress
            }).ToListAsync();
            return (emp);
        }
        public async Task<dynamic> GetEmployeebyJoinDepart()
        {

            var emp = await (from e in _context.employee
                             join d in _context.department
                             on e.DepartmentId equals d.Id
                             select new EmployeeDto
                             {

                                 EAddress = e.EAddress,
                                 DepartmentName = d.DepartmentName,
                                 DepartmentId = e.DepartmentId,
                                 Salary = e.Salary,
                                 IsActive = e.IsActive

                             }).ToListAsync();


            var empLeftJoin = await (from e in _context.employee
                                     join d in _context.department
                                     on e.DepartmentId equals d.Id into gj
                                     from g in gj.DefaultIfEmpty()
                                     select new EmployeeDto
                                     {
                                         EAddress = e.EAddress
                                     ,
                                         Salary = e.Salary,
                                         DepartmentId = e.DepartmentId
                                     ,
                                         DepartmentName = g.DepartmentName,
                                         EName = e.EName
                                     }).ToListAsync();


            // works like left join as we included
            var empMethodmode = await _context.employee.Include(e => e.Department).Select(x => new EmployeeDto
            {
                EName = x.EName,
                DepartmentName = x.Department != null ? x.Department.DepartmentName : null

            }).ToListAsync();

            var empmethodwithinnerjoin = await _context.employee.Where(x => x.Department != null).Include(x => x.Department).Select(x => new EmployeeDto

            {
                EName = x.EName,
                DepartmentName = x.Department != null ? x.Department.DepartmentName : null
            }).ToListAsync();

            return (emp);
        }


    }
}
