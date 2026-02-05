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
        public IActionResult saveEmployee([FromBody] EmployeeDto DTO)
        {

            var empid = _IEmployee.SaveEmployee(DTO);
            return Ok(empid);
        }

        [HttpGet]
        [Route("GetEmployee")]
        public Task<List<EmployeeDto>> GetEmployee()
        {

            return _IEmployee.GetEmployee();
        
        }

    }
}
