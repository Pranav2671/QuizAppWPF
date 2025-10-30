using Microsoft.AspNetCore.Mvc;
using QuizAPI.Data;
using QuizAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace QuizAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        // Hardcoded questions
        //private static readonly List<Question> Questions = new()
        //{
        //    new Question { Id = 1, Text = "Capital of France?", Options = new List<string>{"Paris","London","Berlin","Rome"}, CorrectIndex = 0 },
        //    new Question { Id = 2, Text = "5 + 3 = ?", Options = new List<string>{"6","7","8","9"}, CorrectIndex = 2 },
        //    new Question { Id = 3, Text = "Red planet?", Options = new List<string>{"Venus","Mars","Jupiter","Saturn"}, CorrectIndex = 1 }
        //};

        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: api/questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            return await _context.Questions.Include(q => q.Topic).ToListAsync();
        }

        //GET: api/questions/ByTopic/{id}
        [HttpGet("ByTopic/{topicId}")]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionsByTopic(int topicId)
        {
            var questions = await _context.Questions
                .Where(q => q.TopicId == topicId)
                .ToListAsync();
            return questions;
        }

        //GET: api/questions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if(question == null)
                return NotFound();
            return question;
        }

        // ✅ POST: api/Questions
        [HttpPost]
        public async Task<ActionResult<Question>> CreateQuestion(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
        }

        // ✅ PUT: api/Questions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, Question question)
        {
            if (id != question.Id)
                return BadRequest();

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Questions.Any(q => q.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // ✅ DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
                return NotFound();

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
