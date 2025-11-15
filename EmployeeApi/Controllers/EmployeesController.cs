using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeApi.Data;
using EmployeeApi.Models;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDbContext _db;

        public EmployeesController(EmployeeDbContext db)
        {
            _db = db;
        }

        // GET: /api/employees
        // Optional search: /api/employees?searchString=alice  (matches Name contains)
        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] string? searchString)
        {
            // If no search term -> return all employees
            if (string.IsNullOrWhiteSpace(searchString))
            {
                var allEmployees = await _db.Employees
                    .OrderBy(s => s.Id)
                    .ToListAsync();

                return Ok(allEmployees);
            }

            // If there IS a search term -> filter by Name
            var filtered = await _db.Employees
                .Where(s => s.Name != null && s.Name.Contains(searchString))
                .OrderBy(s => s.Id)
                .ToListAsync();

            return Ok(filtered);
        }

        // GET: /api/employees/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            return employee is null ? NotFound() : Ok(employee);
        }

        // POST: /api/employees
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // PUT: /api/employees/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee updated)
        {
            if (id != updated.Id) return BadRequest("Route id and body id must match.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _db.Employees.FindAsync(id);
            if (existing is null) return NotFound();

            existing.Name = updated.Name;
            existing.Email = updated.Email;
            existing.Phone = updated.Phone;
            existing.DateOfBearth = updated.DateOfBearth;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/employees/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var existing = await _db.Employees.FindAsync(id);
            if (existing is null) return NotFound();

            _db.Employees.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
