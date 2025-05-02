using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cephalog.Practices;

namespace Cephalog.Models
{
    public class TimedTask : BindableBase
    {
        private TimeSpent? currentTimeSpent;
        private TimeSpan totalTimeSpent;
        private string? category;
        private string? client;
        private string? title;

        public ObservableCollection<TimeSpent> TimeSpent { get; set; } = [];
        public TimeSpent? CurrentTimeSpent { get => currentTimeSpent; set => SetProperty(ref currentTimeSpent, value); }
        public TimeSpan TotalTimeSpent { get => totalTimeSpent; set => SetProperty(ref totalTimeSpent, value); }
        public string? Category { get => category; set => SetProperty(ref category, value); }
        public string? Client { get => client; set => SetProperty(ref client, value); }
        public string? Title { get => title; set => SetProperty(ref title, value); }
    }
}