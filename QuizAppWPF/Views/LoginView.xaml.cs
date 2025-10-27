using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using QuizAppWPF.Models;


namespace QuizAppWPF.Views
{
    public partial class LoginView : Window
    {
        private readonly HttpClient _httpClient;

        public LoginView()
        {
            InitializeComponent();

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:5001/"); // Your API URL
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill both feilds.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var loginData = new {Username = username, Password = password };

            string json = JsonConvert.SerializeObject(loginData);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsync("api/Users/login", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseJson = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<dynamic>(responseJson);

                    MessageBox.Show("Login successfull!","Success",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    //Open dashboards based on role
                    if (loginResponse.Role == "Admin")
                    {
                        AdminDashboard admin = new AdminDashboard();
                        admin.Show();
                    }
                    else
                    {
                        UserDashboard user = new UserDashboard();
                        user.Show();
                    }
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Login failed: " + error,
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Connection Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void RegisterText_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("Register page coming soon!");
        }
    }
}
