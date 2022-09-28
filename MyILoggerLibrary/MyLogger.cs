using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.RegularExpressions;

namespace MyILoggerLibrary
{
    public class MyLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return default!;
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            using (Stream outputStream = File.Open("output.log", FileMode.Append))
            using (StreamWriter streamWriter = new StreamWriter(outputStream))
            {
                streamWriter.WriteLine(formatter(state, exception));

            }
        }
    }
}