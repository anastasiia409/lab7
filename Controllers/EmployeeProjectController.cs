using lab7.DAL;
using lab7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProjectController : ControllerBase
    {
        private readonly DbConnection _db;

        public EmployeeProjectController(DbConnection db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeProject>>> GetEmployeeProjects()
        {
            return await _db.EmployeeProjects.OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeProject>> GetEmployeeProject(int id)
        {
            return await _db.EmployeeProjects.FirstAsync(p => p.Id == id);
        }

        [HttpPost]
        public async Task AddEmployeeProject(EmployeeProject eproject)
        {
            await _db.EmployeeProjects.AddAsync(eproject);
            await _db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task EditEmployeeProject(EmployeeProject eproject)
        {
            _db.EmployeeProjects.Update(eproject);
            await _db.SaveChangesAsync();
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteEmployeeProject(int id)
        {
            _db.EmployeeProjects.Remove(await _db.EmployeeProjects.FirstAsync(p => p.Id == id));
            await _db.SaveChangesAsync();
        }
    }
}

