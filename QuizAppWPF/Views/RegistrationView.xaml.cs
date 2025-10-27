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
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
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

            // TODO: Call API to register (we'll add this in the next step)
            MessageBox.Show($"Ready to register: {username} as {role}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoginText_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // go back to login screen
            LoginView login = new LoginView();
            login.Show();
            this.Close();
        }
    }
}
