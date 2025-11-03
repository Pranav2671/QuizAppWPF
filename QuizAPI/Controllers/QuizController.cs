using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Data;
using QuizAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetAllQuizzes()
        {
            var quizzes = await _context.Quizzes
                .Include(q => q.Topic)
                .ToListAsync();

            return Ok(quizzes);
        }

        // ✅ POST: api/quiz
        [HttpPost]
        public async Task<ActionResult<Quiz>> AddQuiz([FromBody] Quiz quiz)
        {
            if (quiz == null || quiz.TopicId <= 0)
                return BadRequest("Invalid quiz data");

            // Ensure Topic exists
            var topicExists = await _context.Topics.AnyAsync(t => t.Id == quiz.TopicId);
            if (!topicExists)
                return NotFound("Topic not found");

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            // Return created quiz
            return CreatedAtAction(nameof(GetAllQuizzes), new { id = quiz.Id }, quiz);
        }

        // ✅ DELETE: api/quiz/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
                return NotFound();

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
