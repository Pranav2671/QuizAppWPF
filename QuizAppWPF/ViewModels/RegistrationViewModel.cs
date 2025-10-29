using QuizAppWPF.Services.Api;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QuizAppWPF.Models;

namespace QuizAppWPF.ViewModels
{
    internal class RegistrationViewModel: INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _confirmPassword;
        private readonly IUserApi _userApi;


        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));

        public RegistrationViewModel(IUserApi userApi)
        {
            _userApi = userApi;
        }

        public async Task<bool> RegisterAsync()
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                MessageBox.Show("Please fill all fields.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            try
            {
                var newUser = new User
                {
                    Username = Username,
                    Password = Password,
                    Role = "User" // default role
                };

                var response = await _userApi.RegisterAsync(newUser); // returns ApiResponse<User>

                if (response.IsSuccessStatusCode || (response != null && response.IsSuccessStatusCode))
                {
                    MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    // Attempt to show server error if present
                    var msg = response?.Error?.Content ?? "Registration failed.";
                    MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (ApiException apiEx)
            {
                // Refit's ApiException contains content and status
                MessageBox.Show($"Registration failed: {apiEx.Content}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
