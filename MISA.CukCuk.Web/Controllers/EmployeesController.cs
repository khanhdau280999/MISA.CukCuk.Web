using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Web.Controllers
{
    public class EmployeesController : BaseEntityController<Employee>
    {
        IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService) :base(employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("filter")]
        public IActionResult FilterEmployee([FromQuery] string inputValue, [FromQuery] Guid? departmentId, [FromQuery] Guid? positionId)
        {
            return Ok(_employeeService.FilterEmployee(inputValue, departmentId, positionId));
        }

        [HttpGet("MaxCode")]
        public IActionResult GetMaxEmployeeCode()
        {
            var maxId = Convert.ToInt64(_employeeService.GetMaxEmployeeCode());
            return Ok(maxId);
        }
    }
}
