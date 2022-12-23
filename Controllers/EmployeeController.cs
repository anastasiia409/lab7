using lab7.DAL;
using lab7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController: ControllerBase
    {
        private readonly DbConnection _db;

        public EmployeeController(DbConnection db)
            {
                _db = db;
            }

            [HttpGet]
            public async Task<ActionResult<List<Employee>>> GetEmployees()
            {
                return await _db.Employees.OrderBy(p => p.Name).ToListAsync();
            }

            [HttpGet("{id:int}")]
            public async Task<ActionResult<Employee>> GetEmployee(int id)
            {
                return await _db.Employees.FirstAsync(p => p.Id == id);
            }

            [HttpPost]
            public async Task AddEmployee(Employee employee)
            {
                await _db.Employees.AddAsync(employee);
                await _db.SaveChangesAsync();
            }

            [HttpPut]
            public async Task EditBook(Employee employee)
            {
                _db.Employees.Update(employee);
                await _db.SaveChangesAsync();
            }

            [HttpDelete("{id:int}")]
            public async Task DeleteBook(int id)
            {
                _db.Employees.Remove(await _db.Employees.FirstAsync(p => p.Id == id));
                await _db.SaveChangesAsync();
            }
        }
}
