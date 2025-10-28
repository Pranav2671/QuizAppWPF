using QuizAppWPF.Models;
using Refit;

namespace QuizAppWPF.Services
{
    public interface IUserApi
    {
        [Post("/api/users/register")]
        Task<ApiResponse<User>> RegisterAsync([Body] User user);

        [Post("/api/users/login")]
        Task<ApiResponse<User>> LoginAsync([Body] User user);
    }
}
