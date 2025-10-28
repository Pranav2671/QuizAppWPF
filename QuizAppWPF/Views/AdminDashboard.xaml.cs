using QuizAppWPF.Services;
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

        private void Logout_Click(object sender, RoutedEventArgs e)
        {

            //Go back to login window
            LoginView login = new LoginView(userApi);
            login.Show();
            this.Close();
        }
    }
}
