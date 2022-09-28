using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyILoggerLibrary
{
    public sealed class MyLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, ILogger> _loggers =
            new ConcurrentDictionary<string, ILogger>(StringComparer.OrdinalIgnoreCase);
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, key => new MyLogger());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
