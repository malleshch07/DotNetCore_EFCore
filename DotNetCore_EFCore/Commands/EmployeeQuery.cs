using DotNetCore_EFCore_CQRS.DTO;
using DotNetCore_EFCore_CQRS.Repositories;

namespace DotNetCore_EFCore_CQRS.Commands
{
    public class EmployeeQuery:IEmployeeQuery
    {

        private readonly IEmployeeQueryRepositoriesService _IEmpQueryrepo;

        public EmployeeQuery(IEmployeeQueryRepositoriesService IEmprepo)
        {

            _IEmpQueryrepo = IEmprepo;

        }
      public async  Task<List<EmployeeDto>> GetEmployeeDataBylinq()
        {

           return await _IEmpQueryrepo.GetEmployeeDataBylinq();

        }
    }
}
