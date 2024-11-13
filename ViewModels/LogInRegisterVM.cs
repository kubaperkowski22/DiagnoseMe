using DiagnoseMe.Helpers;
using DiagnoseMe.Models;
using DiagnoseMe.Tools.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiagnoseMe.ViewModels
{
    public class LogInRegisterVM : INotifyPropertyChanged
    {
        #region Register
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _name;

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }
        private string _surname;

        public EGender Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        private EGender _gender;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string _email;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(ArePasswordsTheSame));
            }
        }
        private string _password;

        public string RepeatedPassword
        {
            get => _repeatedPassword;
            set
            {
                _repeatedPassword = value;
                OnPropertyChanged(nameof(RepeatedPassword));
                OnPropertyChanged(nameof(ArePasswordsTheSame));
            }
        }
        private string _repeatedPassword;

        #endregion

        #region Log In

        public string LoginEmail
        {
            get => _loginEmail;
            set
            {
                _loginEmail = value;
                OnPropertyChanged(nameof(LoginEmail));
            }
        }
        private string _loginEmail;

        public SecureString LoginPassword
        {
            get => _loginPassword;
            set
            {
                _loginPassword = value;
                OnPropertyChanged(nameof(LoginPassword));
            }
        }
        private SecureString _loginPassword;

        #endregion

        public bool ArePasswordsTheSame
        {
            get => Password == RepeatedPassword ? true : false;
        }

        private AppDbContext _database;
        public LogInRegisterVM() 
        {
            _database = App.Current.Services.GetRequiredService<AppDbContext>();
        }

        public async Task LogIn(string email, string password)
        {
            User user = (User)_database.Users.Select(x => x.Email == email && x.Password == password);
            if (user != null)
            {
                var mainWindowVM = App.Current.Services.GetService<MainWindowVM>();

                mainWindowVM.LoggedUser = user;
                mainWindowVM.IsUserLoggedIn = true;
            }
        }

        public async Task AddUserAsync()
        {
            try
            {
                if(_database.Users.Select(x => x.Email == Email) != null)
                {
                    MessageBox.Show("Konto o podanym adresie email już istnieje!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var user = new User(Name, Surname, Gender, Email, Password);

                _database.Users.Add(user);

                await _database.SaveChangesAsync();

                MessageBox.Show("Użytkownik został dodany.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas dodawania użytkownika: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
