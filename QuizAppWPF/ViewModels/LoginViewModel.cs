using System.ComponentModel;
using System.Threading.Tasks;
using QuizAppWPF.Models;
using QuizAppWPF.Services.Api;

namespace QuizAppWPF.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username = "";
        private string _password = "";
        private string _role = "";
        public readonly IUserApi UserApi;

        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Role
        {
            get { return _role; }
            private set
            {
                if (_role != value)
                {
                    _role = value;
                    OnPropertyChanged("Role");
                }
            }
        }

        public LoginViewModel(IUserApi userApi)
        {
            UserApi = userApi;
        }

        public async Task<bool> LoginAsync()
        {
            var loginData = new User { Username = this.Username, Password = this.Password };

            var response = await UserApi.LoginAsync(loginData);
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                Role = response.Content.Role;
                return true;
            }

            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
