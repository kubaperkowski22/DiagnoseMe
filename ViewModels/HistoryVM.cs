using DiagnoseMe.Models;
using DiagnoseMe.Tools.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DiagnoseMe.ViewModels
{
    public class HistoryVM : INotifyPropertyChanged
    {
        private AppDbContext _db;
        public ObservableCollection<DiagnosisResult> DiagnosisResults { get; set; }
        private User _LoggedUser
        {
            get
            {
                if (_loggedUser is null)
                    _loggedUser = App.Current.Services.GetService<MainWindowVM>().LoggedUser;

                return _loggedUser;
            }
        }
        private User _loggedUser;

        public HistoryVM()
        {
            _db = App.Current.Services.GetService<AppDbContext>();
        }

        public async Task DeleteDiagnosisFromHistory(DiagnosisResult diagnosisResult)
        {
            _db.DiagnosisResults.Remove(diagnosisResult);
            DiagnosisResults.Remove(diagnosisResult);

            await _db.SaveChangesAsync();
            OnPropertyChanged(nameof(DiagnosisResults));
        }

        public void RefresHistory()
        {
            if (_LoggedUser is not null)
            {
                var results = _db.DiagnosisResults.Where(x => x.UserId == _LoggedUser.Id).ToList();
                DiagnosisResults = new ObservableCollection<DiagnosisResult>(results);
                OnPropertyChanged(nameof(DiagnosisResults));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
