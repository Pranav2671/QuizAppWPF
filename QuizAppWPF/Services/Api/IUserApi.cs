using QuizAppWPF.Models;
using Refit;

namespace QuizAppWPF.Services.Api
{
    public interface IUserApi
    {
        [Post("/api/users/register")]
        Task<ApiResponse<User>> RegisterAsync([Body] User user);


        [Post("/api/users/login")]
        Task<ApiResponse<User>> LoginAsync([Body] User user);


        [Get("/api/users")]
        Task<List<User>> GetUsersAsync();


        [Delete("/api/users/{id}")]
        Task DeleteUserAsync(int id);
    }
}
