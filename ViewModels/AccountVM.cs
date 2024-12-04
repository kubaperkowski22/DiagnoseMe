using DiagnoseMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DiagnoseMe.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DiagnoseMe.Tools.Data;
using System.Windows;

namespace DiagnoseMe.ViewModels
{
    public class AccountVM : INotifyPropertyChanged
    {
        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _name;

        public string? Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }
        private string _surname;

        public EGender? Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        private EGender? _gender;

        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string _email;

        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string _password;

        public User? User { get; set; }
        private AppDbContext _database;


        public AccountVM()
        {
            User = App.Current.Services.GetService<MainWindowVM>().LoggedUser;

            _database = App.Current.Services.GetRequiredService<AppDbContext>();

            SetUserProperties();
        }

        private void SetUserProperties()
        {
            if(User is null) return;
            
            Name = User.Name;
            Surname = User.Surname;
            Gender = User.Gender;
            Email = User.Email;
            Password = User.Password;
        }

        public async Task DeleteUser()
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć konto?", "Potwierdź operację", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                return;

            try
            {
                DeleteAllUserData(User.Id);

                _database.Remove(User);

                await _database.SaveChangesAsync();

                App.Current.Services.GetService<MainWindowVM>().LoggedUser = null;

                MessageBox.Show("Pomyślnie usunięto użytkownika.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Nie udało się usunąć użytkownika.\n{ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task DeleteAllUserData(int userId)
        {
            foreach (var diagnosis in _database.DiagnosisResults)
            {
                if(diagnosis.UserId == userId)
                    _database.Remove(diagnosis);
            }

            foreach (var appointment in _database.Appointments)
            {
                if (appointment.UserId == userId)
                    _database.Remove(appointment);
            }

            await _database.SaveChangesAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
