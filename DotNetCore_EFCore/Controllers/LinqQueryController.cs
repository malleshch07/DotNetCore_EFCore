using DotNetCore_EFCore_CQRS.Commands;
using DotNetCore_EFCore_CQRS.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            var emp = await _IEmpQuery.GetEmployeeDataBylinq();

            return emp;
        }

        [HttpGet]
        [Route("GetEmployeeDetailsById/{id}")]
        public async Task<IActionResult> GetEmployeeDetailsById(int id)
        {

            var emp = await _IEmpQuery.GetEmployeeDetailsById(id);

            if (emp == null)
                return NotFound();
            return Ok(emp);
        }

        [HttpGet]
        [Route("GetEmployeeDetailsBydynamicSort/{sortby}")]
        public async Task<IActionResult> GetEmployeeDetailsBydynamicSort(string  sortby)
        {

            var emp = await _IEmpQuery.GetEmployeeDetailsBydynamicSort(sortby);

            if (emp == null)
                return NotFound();
            return Ok(emp);
        }


        [HttpGet]
        [Route("GetEmployeebyPageSize")]
        public async Task<IActionResult> GetEmployeebyPageSize(int Pagesize, int pagenumber)
        {

            var emp = await _IEmpQuery.GetEmployeebyPageSize(pagenumber, Pagesize);

            if (emp == null)
                return NotFound();
            return Ok(emp);
        }
        [HttpGet]
        [Route("GetEmployeebyCount_Any_LongCount")]
        public async Task<IActionResult> GetEmployeebyCount_Any_LongCount(string name)
        {

            var emp = await _IEmpQuery.GetEmployeebyCount_Any_LongCount( name);

            if (emp == null)
                return NotFound();
            return Ok(emp);
        }
        [HttpGet]
        [Route("GetEmployeebyFirstOrSingle/{name}")]
        public async Task<IActionResult> GetEmployeebyFirstOrSingle(string name)
        {

            var emp = await _IEmpQuery.GetEmployeebyFirstOrSingle( name);

            if (emp == null)
                return NotFound();
            return Ok(emp);
        }


        [HttpGet]
        [Route("GetEmployeebyJoinDepart")]
        public async Task<IActionResult> GetEmployeebyJoinDepart()
        {

            var emp = await _IEmpQuery.GetEmployeebyJoinDepart();

            if (emp == null)
                return NotFound();
            return Ok(emp);
        }

        [HttpGet]
        [Route("GetEmployeebyMutliJoin")]
        public async Task<IActionResult> GetEmployeebyMutliJoin()
        {

            var emp = await _IEmpQuery.GetEmployeebyMutliJoin();

            if (emp == null)
                return NotFound();
            return Ok(emp);
        }
        [HttpGet]
        [Route("GetEmployeebyGroupBy")]
        public async Task<IActionResult> GetEmployeebyGroupBy()
        {

            var emp = await _IEmpQuery.GetEmployeebyGroupBy();

            if (emp == null)
                return NotFound();
            return Ok(emp);
        }
    }
}
