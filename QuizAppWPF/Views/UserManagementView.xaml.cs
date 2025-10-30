using QuizAppWPF.Models;
using QuizAppWPF.Services.Api;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace QuizAppWPF.Views.Admin
{
    public partial class UserManagementView : Window
    {
        private readonly IUserApi _userApi;

        public UserManagementView()
        {
            InitializeComponent();

            // Create Refit instance for IUserApi
            _userApi = RestService.For<IUserApi>("https://localhost:5001"); // 🔹 Change if your API port is different

            // Load users when window opens
            Loaded += async (s, e) => await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await _userApi.GetUsersAsync();

                //Filter out Admin
                var filteredUsers = users.Where(u => u.Role != null && u.Role.Equals("User", StringComparison.OrdinalIgnoreCase)).ToList();

                UsersGrid.ItemsSource = filteredUsers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching users: {ex.Message}");
            }
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadUsersAsync();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is User selectedUser)
            {
                var confirm = MessageBox.Show($"Delete user '{selectedUser.Username}'?",
                                              "Confirm Delete",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Warning);

                if (confirm == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _userApi.DeleteUserAsync(selectedUser.Id);
                        MessageBox.Show("User deleted successfully!");
                        await LoadUsersAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete user: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }
    }
}
