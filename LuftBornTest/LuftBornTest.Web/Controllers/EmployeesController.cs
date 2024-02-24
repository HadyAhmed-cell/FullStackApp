using LuftBornTest.Core.Entities;
using LuftBornTest.Core.Interfaces;
using LuftBornTest.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuftBornTest.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository _repository;

        public EmployeesController(IRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddAsync([FromBody] Employee employee)
        {
            await _repository.AddAsync(employee);
            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Employee>> GetById([FromRoute] int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee([FromRoute] int id, Employee updateEmployeeRequest)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Department = updateEmployeeRequest.Department;

            await _repository.UpdateAsync(employee);

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteEmployee([FromRoute] int id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}