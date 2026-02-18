using DotNetCore_EFCore_CQRS.Commands;
using DotNetCore_EFCore_CQRS.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_EFCore_CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinqQueryController : ControllerBase
    {

        private readonly IEmployeeQuery _IEmpQuery;
        public LinqQueryController(IEmployeeQuery IEmpqry)
        {

            _IEmpQuery = IEmpqry;
        }


        [HttpGet]
        [Route("GetEmployeeDataBylinq")]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployeeDataBylinq()
        {
           var emp=await _IEmpQuery.GetEmployeeDataBylinq();

            return Ok(emp);
        }
    }
}
