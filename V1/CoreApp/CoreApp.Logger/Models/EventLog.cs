using System;

namespace CoreApp.Logger.Models
{
    public class EventLog
    {
        public long Id { get; set; }
        public int EventId { get; set; }
        public string Logger { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
