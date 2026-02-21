using DotNetCore_EFCore_CQRS.DTO;

namespace DotNetCore_EFCore_CQRS.Repositories
{
    public interface IEmployeeQueryRepositoriesService
    {
        Task<List<EmployeeDto>> GetEmployeeDataBylinq();

        Task<EmployeeDto> GetEmployeeDetailsById(int id);
        Task<EmployeeDto> GetEmployeeDetailsBydynamicSort(string sortby);
        Task<dynamic> GetEmployeebyPageSize(int pagenumber, int Pagesize);
        Task<dynamic> GetEmployeebyCount_Any_LongCount(string name);
        Task<dynamic> GetEmployeebyFirstOrSingle(string name);
        Task<dynamic> GetEmployeebyJoinDepart();
    }
}
