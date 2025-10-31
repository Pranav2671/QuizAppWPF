using QuizAppWPF.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuizAppWPF.Models
{
    public class Topic
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        // We can include this if we’ll show or manage questions for each topic

        [JsonIgnore]
        public List<Question> Questions { get; set; }
    }
}
