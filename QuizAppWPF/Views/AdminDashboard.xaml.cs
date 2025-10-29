using QuizAppWPF.Services.Api;
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

        //This method runs when the admin clicks the "Manage Users" button

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Recreate the LoginView
            var loginView = new LoginView(userApi);
            loginView.Show();

            // Close current dashboard
            this.Close();
        }
    }
}
