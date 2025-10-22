using Microsoft.AspNetCore.Mvc;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        // Hardcoded questions
        private static readonly List<Question> Questions = new()
        {
            new Question { Id = 1, Text = "Capital of France?", Options = new List<string>{"Paris","London","Berlin","Rome"}, CorrectIndex = 0 },
            new Question { Id = 2, Text = "5 + 3 = ?", Options = new List<string>{"6","7","8","9"}, CorrectIndex = 2 },
            new Question { Id = 3, Text = "Red planet?", Options = new List<string>{"Venus","Mars","Jupiter","Saturn"}, CorrectIndex = 1 }
        };

        [HttpGet]
        public IActionResult GetQuestions()
        {
            return Ok(Questions);  // Return all questions as JSON
        }
    }
}
