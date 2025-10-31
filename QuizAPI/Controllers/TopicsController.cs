using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Data;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TopicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: api/topics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topic>>> GetTopics()
        {
            return await _context.Topics.ToListAsync();
        }

        //GET: api/Topics/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> GetTopic(int id)
        {
            var topic = await _context.Topics.FindAsync(id);

            if (topic == null)
                return NotFound();

            return topic;
        }

        //POST: api/Topics
        [HttpPost]
        public async Task<IActionResult> CreateTopic(Topic topic)
        {
            if (string.IsNullOrWhiteSpace(topic.Name))
            {
                return BadRequest("Topic name is required.");
            }

            topic.CreatedAt = DateTime.UtcNow;

            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();

            return Ok(topic);
        }


        // ✅ PUT: api/Topics/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTopic(int id, Topic topic)
        {
            if (id != topic.Id)
            {
                return BadRequest();
            }

            _context.Entry(topic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Topics.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        // ✅ DELETE: api/Topics/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
                return NotFound();

            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
