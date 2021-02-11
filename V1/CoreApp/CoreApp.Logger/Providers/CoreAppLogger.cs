using CoreApp.Logger.Helpers;
using CoreApp.Logger.Models;
using Microsoft.Extensions.Logging;
using System;

namespace CoreApp.Logger.Providers
{
    public class CoreAppLogger : ILogger
    {
        private string _categoryName;
        private Func<string, LogLevel, bool> _filter;
        private SqlHelper _helper;
        private int MessageMaxLength = 4000;

        public CoreAppLogger(string categoryName, Func<string, LogLevel, bool> filter, string connectionString)
        {
            _categoryName = categoryName;
            _filter = filter;
            _helper = new SqlHelper(connectionString);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message))
                return;

            if (exception != null)
                message += "\n" + exception.ToString();

            message = message.Length > MessageMaxLength ? message.Substring(0, MessageMaxLength) : message;
            EventLog eventLog = new EventLog
            {
                Message = message,
                EventId = eventId.Id,
                Logger = nameof(TState),
                LogLevel = logLevel.ToString(),
                CreatedDate = DateTime.UtcNow
            };
            _helper.InsertLog(eventLog);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filter == null || _filter(_categoryName, logLevel));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
