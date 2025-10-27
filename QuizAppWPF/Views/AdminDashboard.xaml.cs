using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuizAppWPF.Views
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        //This method runs when the admin clicks the "Manage Users" button

        private void Logout_Click(object sender, RoutedEventArgs e)
        {

            //Go back to login window
            LoginView login = new LoginView();
            login.Show();
            this.Close();
        }
    }
}
