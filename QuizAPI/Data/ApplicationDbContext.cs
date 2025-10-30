using Microsoft.EntityFrameworkCore;
using QuizAPI.Models;

namespace QuizAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Example DbSet (your tables)
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One-to-Many: Topic to Questions
            modelBuilder.Entity<Question>()
                .HasOne(q=> q.Topic)
                .WithMany(t=> t.Questions)
                .HasForeignKey(q=> q.TopicId);
        }
    }
}
