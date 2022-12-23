using lab7.DAL;
using lab7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly DbConnection _db;

        public ProjectController(DbConnection db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetProjects()
        {
            return await _db.Projects.OrderBy(p => p.Name).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            return await _db.Projects.FirstAsync(p => p.Id == id);
        }

        [HttpPost]
        public async Task AddProject(Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task EditProject(Project project)
        {
            _db.Projects.Update(project);
            await _db.SaveChangesAsync();
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteProject(int id)
        {
            _db.Projects.Remove(await _db.Projects.FirstAsync(p => p.Id == id));
            await _db.SaveChangesAsync();
        }
    }
}

