using System.Linq;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFLab
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public RelayCommand _monthValueChangedCommand { get; }

        public MainViewModel()
        {
            InitializeCollections();

            _monthValueChangedCommand = new RelayCommand(MonthValueChangedCommand);
        }

        private void MonthValueChangedCommand(object obj)
        {
        }

        public ObservableCollection<Button> MonthsCollection
        {
            get;
            set;
        }

        public ObservableCollection<Button> DaysCollection
        {
            get;
            set;
        }

        public ObservableCollection<Button> YearsCollection
        {
            get;
            set;
        }

        private string _selectedMonth;
        public string SelectedMonth
        {
            get { return _selectedMonth; }
            set { _selectedMonth = value; RaisePropertyChanged(); }
        }

        private void InitializeCollections()
        {
            string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Where(m => !string.IsNullOrEmpty(m)).ToArray();
            int currentYear = DateTime.Now.Year;

            MonthsCollection = new ObservableCollection<Button>();
            foreach (var element in monthNames.Select(m => new Button() { Content = m }).ToList())
            {
                MonthsCollection.Add(element);
            }

            DaysCollection = new ObservableCollection<Button>();
            foreach (var element in Enumerable.Range(1, 31)
                .Select(m => new Button() { Content = m.ToString() }).ToList())
            {
                DaysCollection.Add(element);
            }
            
            YearsCollection = new ObservableCollection<Button>();
            foreach (var element in Enumerable.Range(currentYear - 110, 111)
                .Select(m => new Button() { Content = m.ToString() }).ToList())
            {
                YearsCollection.Add(element);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
