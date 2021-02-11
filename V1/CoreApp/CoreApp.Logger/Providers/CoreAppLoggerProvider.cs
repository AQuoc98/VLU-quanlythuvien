using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Logger.Providers
{
    public class CoreAppLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private string _connectionString;

        public CoreAppLoggerProvider(Func<string, LogLevel, bool> filter, string connectionStr)
        {
            _filter = filter;
            _connectionString = connectionStr;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CoreAppLogger(categoryName, _filter, _connectionString);
        }

        public void Dispose()
        {

        }
    }
}
