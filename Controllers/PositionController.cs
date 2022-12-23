using lab7.DAL;
using lab7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly DbConnection _db;

        public PositionController(DbConnection db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Position>>> GetPositions()
        {
            return await _db.Positions.OrderBy(p => p.Name).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Position>> GetPosition(int id)
        {
            return await _db.Positions.FirstAsync(p => p.Id == id);
        }

        [HttpPost]
        public async Task AddPosition(Position position)
        {
            await _db.Positions.AddAsync(position);
            await _db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task EditPosition(Position position)
        {
            _db.Positions.Update(position);
            await _db.SaveChangesAsync();
        }

        [HttpDelete("{id:int}")]
        public async Task DeletePosition(int id)
        {
            _db.Positions.Remove(await _db.Positions.FirstAsync(p => p.Id == id));
            await _db.SaveChangesAsync();
        }
    }
}
