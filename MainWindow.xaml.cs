using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cephalog.BusinessLogic;
using Cephalog.Models;

namespace Cephalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _typedDataContext = MainWindowViewModel.Instance;
            _typedDataContext.CurrentDateTime = DateTime.Now;
            var storedData = BusinessService.Instance.GetData(DateTime.Now);
            _typedDataContext.TodayTasks.Clear();
            storedData.ForEach(_typedDataContext.TodayTasks.Add);
            var categories = BusinessService.Instance.GetCategoryList();
            categories.ForEach(_typedDataContext.PossibleCategories.Add);
            var clients = BusinessService.Instance.GetCientList();
            clients.ForEach(_typedDataContext.PossibleClients.Add);
        }

        private readonly MainWindowViewModel _typedDataContext;

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (_typedDataContext.CurrentTask == null) return;
            if (_typedDataContext.CurrentTask.TimeSpent.Count == 0)
            {
                _typedDataContext.TodayTasks.Add(_typedDataContext.CurrentTask);
            }
            if (_typedDataContext.CurrentTask.CurrentTimeSpent == null)
            {
                _typedDataContext.CurrentTask.CurrentTimeSpent = new TimeSpent
                {
                    StartTime = DateTimeOffset.Now,
                    EndTime = null,
                    Timespan = null,
                };
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if (_typedDataContext.CurrentTask == null || _typedDataContext.CurrentTask.CurrentTimeSpent == null) return;
            _typedDataContext.CurrentTask.CurrentTimeSpent.EndTime = DateTimeOffset.Now;
            var realTimespan = DateTimeOffset.Now - _typedDataContext.CurrentTask.CurrentTimeSpent.StartTime;
            _typedDataContext.CurrentTask.CurrentTimeSpent.Timespan = realTimespan;
            _typedDataContext.CurrentTask.TotalTimeSpent += TimeSpan.FromMinutes(BusinessService.Instance.CeilToMinuteDivision(realTimespan, 15) * 15);
            _typedDataContext.CurrentTask.TimeSpent.Add(_typedDataContext.CurrentTask.CurrentTimeSpent);
            _typedDataContext.CurrentTask.CurrentTimeSpent = null;
            BusinessService.Instance.StoreData(_typedDataContext);
        }

        private void ListTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox)?.SelectedItem is TimedTask selectedTask)
            {
                _typedDataContext.CurrentTask = selectedTask;
            }
        }

        private void NewTimedTask(object sender, RoutedEventArgs e)
        {
            if (_typedDataContext == null) return;
            _typedDataContext.CurrentTask = new TimedTask();
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as Calendar)?.SelectedDate is DateTime selectedDate)
            {
                BusinessService.Instance.StoreData(_typedDataContext);
                _typedDataContext.CurrentDateTime = selectedDate;
                _typedDataContext.CurrentTask = null;
                var storedData = BusinessService.Instance.GetData(selectedDate);
                _typedDataContext.TodayTasks.Clear();
                storedData.ForEach(_typedDataContext.TodayTasks.Add);
            }
        }

        private void AddCategory(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_typedDataContext.NewCategory)) return;
            if (_typedDataContext.PossibleCategories.Contains(_typedDataContext.NewCategory)) return;
            _typedDataContext.PossibleCategories.Add(_typedDataContext.NewCategory);
            BusinessService.Instance.StoreCategoryList(_typedDataContext.PossibleCategories.ToList());
        }

        private void AddClient(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_typedDataContext.NewClient)) return;
            if (_typedDataContext.PossibleClients.Contains(_typedDataContext.NewClient)) return;
            _typedDataContext.PossibleClients.Add(_typedDataContext.NewClient);
            BusinessService.Instance.StoreCientList(_typedDataContext.PossibleClients.ToList());
        }

        private void OnEditStartTime(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter
                && e.Source is TextBox tb && TimeSpan.TryParse(tb.Text, out TimeSpan timeSpan)
                && tb.DataContext is TimeSpent ts
                && (ts.StartTime.Hour != timeSpan.Hours || ts.StartTime.Minute != timeSpan.Minutes))
            {
                var newStartTime = ts.StartTime.Date + timeSpan;
                ts.StartTime = newStartTime;
                BusinessService.Instance.RecomputeTimeSpent(_typedDataContext.TodayTasks.ToList());
                BusinessService.Instance.StoreData(_typedDataContext);
            }
        }

        private void OnEditEndTimeTime(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter
                && e.Source is TextBox tb && TimeSpan.TryParse(tb.Text, out TimeSpan timeSpan)
                && tb.DataContext is TimeSpent ts && ts.EndTime.HasValue
                && (ts.EndTime.Value.Hour != timeSpan.Hours || ts.EndTime.Value.Minute != timeSpan.Minutes))
            {
                var newEndTime = ts.EndTime.Value.Date + timeSpan;
                ts.EndTime = newEndTime;
                BusinessService.Instance.RecomputeTimeSpent(_typedDataContext.TodayTasks.ToList());
                BusinessService.Instance.StoreData(_typedDataContext);
            }

        }
    }
}