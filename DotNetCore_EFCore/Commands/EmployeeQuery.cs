using DotNetCore_EFCore_CQRS.DTO;
using DotNetCore_EFCore_CQRS.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace DotNetCore_EFCore_CQRS.Commands
{
    public class EmployeeQuery : IEmployeeQuery
    {

        private readonly IEmployeeQueryRepositoriesService _IEmpQueryrepo;

        public EmployeeQuery(IEmployeeQueryRepositoriesService IEmprepo)
        {

            _IEmpQueryrepo = IEmprepo;

        }
        public async Task<List<EmployeeDto>> GetEmployeeDataBylinq()
        {

            return await _IEmpQueryrepo.GetEmployeeDataBylinq();

        }

        public async Task<EmployeeDto> GetEmployeeDetailsById(int id)
        {

         return  await _IEmpQueryrepo.GetEmployeeDetailsById(id);
        }
        public async Task<EmployeeDto> GetEmployeeDetailsBydynamicSort(string sortby)
        {

         return  await _IEmpQueryrepo.GetEmployeeDetailsBydynamicSort(sortby);
        }

        public async Task<dynamic> GetEmployeebyPageSize(int pagenumber, int Pagesize)
        {


            return await _IEmpQueryrepo.GetEmployeebyPageSize( pagenumber, Pagesize);
        }
        public async Task<dynamic> GetEmployeebyCount_Any_LongCount(string name)
        {


            return await _IEmpQueryrepo.GetEmployeebyCount_Any_LongCount( name);
        }
        public async Task<dynamic> GetEmployeebyFirstOrSingle(string name)
        {


            return await _IEmpQueryrepo.GetEmployeebyFirstOrSingle( name);
        }

        public async Task<dynamic> GetEmployeebyJoinDepart()
        {


            return await _IEmpQueryrepo.GetEmployeebyJoinDepart();
        }
    }
}
