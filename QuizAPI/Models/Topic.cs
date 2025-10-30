using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Models
{
    public class Topic
    {
        // Primary key
        public int Id { get; set; }

        // Name of the topic/category (e.g., "Math", "Science")
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        // When the topic was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property: one Topic has many Questions
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
