using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cephalog.Practices;

namespace Cephalog.Models
{
    public class TimeSpent : BindableBase
    {
        private DateTimeOffset startTime;
        private DateTimeOffset? endTime;
        private TimeSpan? timespan;

        public DateTimeOffset StartTime { get => startTime; set => SetProperty(ref startTime, value); }
        public DateTimeOffset? EndTime { get => endTime; set => SetProperty(ref endTime, value); }
        public TimeSpan? Timespan { get => timespan; set => SetProperty(ref timespan, value); }
    }
}
