using System.Collections.Generic;

namespace QuizAppWPF.Models
{
    public class Question
    {
        // Unique ID of the question (matches API)
        public int Id { get; set; }

        // The text of the question
        public string Text { get; set; }

        // The list of possible answer options
        public List<string> Options { get; set; }

        // Index (number) of the correct answer in the list
        public int CorrectIndex { get; set; }

        // Constructor ensures Text and Options are never null
        public Question()
        {
            Text = "";
            Options = new List<string>();
        }
    }
}
