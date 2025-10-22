using System.Collections.Generic;

namespace QuizAppWPF.Models
{
    public class Question
    {
        public int Id { get; set; }                 // Must match API
        public string Text { get; set; } = "";      // Question text
        public List<string> Options { get; set; } = new(); // Answer options
        public int CorrectIndex { get; set; }       // Correct option index
    }
}
