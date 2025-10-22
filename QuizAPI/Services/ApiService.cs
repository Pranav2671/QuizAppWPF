namespace QuizAPI.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001/")
            };
        }
    }
}
