using System.Windows;
using QuizAppWPF.Services.Api;
using QuizAppWPF.ViewModels;

namespace QuizAppWPF.Views
{
    public partial class RegistrationView : Window
    {
        private readonly RegistrationViewModel _viewModel;

        public RegistrationView(IUserApi userApi)
        {
            InitializeComponent();
            _viewModel = new RegistrationViewModel(userApi);
            DataContext = _viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = PasswordBox.Password;
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.ConfirmPassword = ConfirmPasswordBox.Password;
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var success = await _viewModel.RegisterAsync();
            if (success)
            {
                // Optional: return to login window after successful registration
                var login = new LoginView(_viewModel.GetType().GetProperty("_userApi")?.GetValue(_viewModel) as IUserApi);
                login.Show();
                this.Close();
            }
        }
    }
}
