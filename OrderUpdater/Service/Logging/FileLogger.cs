using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OrderUpdater.Service.Logging
{
    /// <summary>
    /// Логирование возникших проблем в файл с задержкой.
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly TimeSpan _latency = TimeSpan.FromSeconds(10); // 10 секунд
        protected readonly FileLoggerProvider _fileProvider;

        public FileLogger(FileLoggerProvider roundTheCodeLoggerFileProvider)
        {
            _fileProvider = roundTheCodeLoggerFileProvider;
        }

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel) => logLevel == LogLevel.Information;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            Task.Run(async() =>
            {
                try
                {
                    var fullFilePath = _fileProvider.Options.FolderPath + "/" + _fileProvider.Options.FilePath.Replace("{date}", DateTimeOffset.UtcNow.ToString("yyyyMMdd"));
                    var logRecord = string.Format("{0} [{1}] {2} {3}", "[" + DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss+00:00") + "]", logLevel.ToString(), formatter(state, exception), exception != null ? exception.StackTrace : "");

                    using (var streamWriter = new StreamWriter(fullFilePath, true))
                    {
                        await streamWriter.WriteLineAsync(logRecord);
                        Thread.Sleep(_latency);
                    }
                }
                catch (Exception e)
                {
                }
            });
        }
    }

    public class FileLoggerOptions
    {
        public virtual string FilePath { get; set; }

        public virtual string FolderPath { get; set; }
    }
}
