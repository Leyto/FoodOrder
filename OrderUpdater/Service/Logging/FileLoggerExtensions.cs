using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderUpdater.Service.Logging;
using System;

namespace OrderUpdater.Service
{
    public static class RoundTheCodeFileLoggerExtensions
    {
        public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, Action<FileLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
