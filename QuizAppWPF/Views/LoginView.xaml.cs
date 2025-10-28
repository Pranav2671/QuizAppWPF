using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using QuizAppWPF.Models;
using QuizAppWPF.Services;


namespace QuizAppWPF.Views
{
    public partial class LoginView : Window
    {
        private readonly HttpClient _httpClient;
        private readonly IUserApi _userApi;

        public LoginView(IUserApi userApi){
            InitializeComponent();
            _userApi = userApi;
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

            var loginData = new User
            {
                Username = username,
                Password = password
            };

            

            try
            {
                var response= await _userApi.LoginAsync(loginData);
                //HttpResponseMessage response =
                //    await _httpClient.PostAsync("api/Users/login", content);

                if (response.IsSuccessful)
                {
                    
                    MessageBox.Show("Login successfull!","Success",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    //Open dashboards based on role
                    if (response.Content.Role == "Admin")
                    {
                        AdminDashboard admin = new AdminDashboard(_userApi);
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
                    MessageBox.Show("Login failed",
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
            RegistrationView reg = new RegistrationView(_userApi);
            reg.Show();
            this.Close();
        }

    }
}
