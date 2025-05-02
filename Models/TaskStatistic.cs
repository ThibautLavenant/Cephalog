using Cephalog.Practices;

namespace Cephalog.Models
{
    public class TaskStatistic : BindableBase
    {
        private string? category;
        private string? client;
        private TimeSpan totalTimeSpent;
        public string? Category { get => category; set => SetProperty(ref category, value); }
        public string? Client { get => client; set => SetProperty(ref client, value); }
        public TimeSpan TotalTimeSpent { get => totalTimeSpent; set => SetProperty(ref totalTimeSpent, value); }
    }
}