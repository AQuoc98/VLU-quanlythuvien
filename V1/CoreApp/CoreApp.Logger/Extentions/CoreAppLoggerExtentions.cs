using CoreApp.Logger.Providers;
using Microsoft.Extensions.Logging;
using System;

namespace CoreApp.Logger.Extentions
{
    public static class CoreAppLoggerExtentions
    {
        /// <summary>
        /// Add Context
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="filter"></param>
        /// <param name="connectionStr"></param>
        /// <returns></returns>
        public static ILoggerFactory AddContext(this ILoggerFactory factory, Func<string, LogLevel, bool> filter = null, string connectionStr = null)
        {
            factory.AddProvider(new CoreAppLoggerProvider(filter, connectionStr));
            return factory;
        }

        /// <summary>
        /// Add Context
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="minLevel"></param>
        /// <param name="connectionStr"></param>
        /// <returns></returns>
        public static ILoggerFactory AddContext(this ILoggerFactory factory, LogLevel minLevel, string connectionStr)
        {
            return AddContext(factory,(_, logLevel) => logLevel >= minLevel, connectionStr);
        }
    }
}
