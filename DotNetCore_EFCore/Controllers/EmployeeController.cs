using DotNetCore_EFCore_CQRS.Commands;
using DotNetCore_EFCore_CQRS.DTO;
using DotNetCore_EFCore_CQRS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_EFCore_CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeCommands _IEmployee;
        public EmployeeController(IEmployeeCommands Iemployee)
        {
            _IEmployee = Iemployee;

        }



        [HttpPost]
        [Route("SaveEmployee")]
        public async Task<IActionResult> saveEmployee([FromBody] EmployeeDto DTO)
        {

            var empid = await _IEmployee.SaveEmployee(DTO);
            return Ok(empid);
        }

        [HttpGet]
        [Route("GetEmployee")]
        public async Task<List<EmployeeDto>> GetEmployee()
        {

            return await _IEmployee.GetEmployee();

        }

        [HttpGet]
        [Route("GetEmployeeByDepartment/")]
        public async Task<List<EmployeeDto>> GetEmployeewithDepartment([FromQuery] int? deptId)
        {
            return await _IEmployee.GetEmployeewithDepartment(deptId);
        }

        //[HttpGet]
        //[Route("GetEmployeewithDepartwithEFlazyload")]
        //public async Task<List<EmployeeDto>> GetEmployeewithDepartwithEFlazyload()
        //{
        //    return await _IEmployee.GetEmployeewithDepartwithEFlazyload();
        //}


        [HttpGet]
        [Route("GetEmployeewithDepartwithEFExplictLoading")]
        public async Task<List<EmployeeDto>> GetEmployeewithDepartwithEFExplictLoading()
        {
            return await _IEmployee.GetEmployeewithDepartwithEFExplictLoading();
        }

    }
}
