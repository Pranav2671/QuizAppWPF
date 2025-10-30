using QuizAppWPF.Services.Api;
using QuizAppWPF.Views.Admin;
using System.Windows;

namespace QuizAppWPF.Views
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        private readonly IUserApi userApi;
        public AdminDashboard(IUserApi user)
        {
            InitializeComponent();
            userApi = user;
           
        }

        

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Recreate the LoginView
            var loginView = new LoginView(userApi);
            loginView.Show();

            // Close current dashboard
            this.Close();
        }

        private void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
            UserManagementView manageUsers = new UserManagementView();
            manageUsers.ShowDialog();
        }

    }
}
