using System.Windows;
using System.Windows.Controls;
using QuizAppWPF.Services;
using Refit;

namespace QuizAppWPF.Views
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        private readonly IUserApi _userApi;

        public RegistrationView(IUserApi userApi)
        {
            InitializeComponent();
            _userApi = userApi;
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Register button clicked!");

            //For now well do basic validation here
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            string confirm = ConfirmPasswordBox.Password.Trim();
            var roleItem =(ComboBoxItem)RoleComboBox.SelectedItem;
            string role = roleItem.Content.ToString() ?? "User";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill username and password.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newUser = new QuizAppWPF.Models.UserRegisterModel
            {
                Username = username,
                Password = password,
                Role = role
            };

            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new System.Uri("https://localhost:7024/");
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(newUser);
                    var content = new System.Net.Http.StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("api/users/register", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Registration successful! You can now log in.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoginView login = new LoginView(_userApi);
                        login.Show();
                        this.Close();
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Registration failed: " + error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            //// TODO: Call API to register (we'll add this in the next step)
            //MessageBox.Show($"Ready to register: {username} as {role}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoginText_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // go back to login screen
            LoginView login = new LoginView(_userApi);
            login.Show();
            this.Close();
        }
    }
}
