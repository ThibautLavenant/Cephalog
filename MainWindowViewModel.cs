using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cephalog.Models;
using Cephalog.Practices;

namespace Cephalog
{
    public class MainWindowViewModel : BindableBase
    {
        public static MainWindowViewModel Instance { get; } = new MainWindowViewModel();
        private readonly ObservableCollection<TimedTask> todayTasks = [];
        private TimedTask? currentTask;

        public DateTime CurrentDateTime { get; set; }
        public ObservableCollection<TimedTask> TodayTasks => todayTasks;
        public TimedTask? CurrentTask { get => currentTask; set => SetProperty(ref currentTask, value); }
        public ObservableCollection<string> PossibleCategories { get; } = [];
        public ObservableCollection<string> PossibleClients { get; } = [];
        public string NewCategory { get; set; }
        public string NewClient { get; set; }

        private MainWindowViewModel() { }
    }
}
