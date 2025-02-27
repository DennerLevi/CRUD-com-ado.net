using CRUD.BUS;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeBUS _bus;

        // Supondo que você injete via appsettings ou algo assim:
        private readonly string _connectionString = "SuaConnectionStringAqui";

        public EmployeeController()
        {
            _bus = new EmployeeBUS(_connectionString);
        }

        [HttpGet("ReturnEmployees")]
        public ActionResult<List<EmployeeModel>> ReturnEmployees()
        {
            try
            {
                var list = _bus.GetAllEmployees();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("FilterEmployees")]
        public ActionResult<List<EmployeeModel>> FilterEmployees(
            [FromQuery] string name,
            [FromQuery] string lastName,
            [FromQuery] int? status)
        {
            try
            {
                var list = _bus.FilterEmployees(name, lastName, status);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateEmployees")]
        public IActionResult CreateEmployees([FromBody] EmployeeModel model)
        {
            try
            {
                int newId = _bus.CreateEmployee(model);
                return Ok(new { success = true, newId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateEmployees")]
        public IActionResult UpdateEmployees([FromBody] EmployeeModel model)
        {
            try
            {
                int rowsAffected = _bus.UpdateEmployee(model);
                return Ok(new { success = rowsAffected > 0, rowsAffected });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("InactiveEmployees")]
        public IActionResult InactiveEmployees([FromBody] int id)
        {
            try
            {
                int rowsAffected = _bus.InactiveEmployee(id);
                return Ok(new { success = rowsAffected > 0, rowsAffected });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteEmployees")]
        public IActionResult DeleteEmployees([FromBody] int id)
        {
            try
            {
                int rowsAffected = _bus.DeleteEmployee(id);
                return Ok(new { success = rowsAffected > 0, rowsAffected });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
