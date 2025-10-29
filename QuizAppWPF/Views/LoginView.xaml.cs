using System;
using System.Windows;
using System.Windows.Controls;
using QuizAppWPF.Services.Api;
using QuizAppWPF.ViewModels;

namespace QuizAppWPF.Views
{
    public partial class LoginView : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginView(IUserApi userApi)
        {
            InitializeComponent();
            _viewModel = new LoginViewModel(userApi);
            DataContext = _viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (_viewModel != null)
                _viewModel.Password = passwordBox.Password;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var success = await _viewModel.LoginAsync();

            if (success)
            {
                MessageBox.Show("Login successful!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                if (_viewModel.Role == "Admin")
                {
                    var admin = new AdminDashboard(_viewModel.UserApi);
                    admin.Show();
                }
                else
                {
                    var user = new UserDashboard(_viewModel.UserApi);
                    user.Show();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegisterText_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var reg = new RegistrationView(_viewModel.UserApi);
            reg.Show();
            this.Close();
        }
    }
}
