using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Logging
{
    public class Logger : IDisposable
    {


        #region Fields
        private HashSet<LogProvider> providers = new HashSet<LogProvider>();
        #endregion

        #region Properties
        public LogSeverity SeverityLimit { get; set; } = LogSeverity.Verbose;
        #endregion

        #region Provider Methods

        public void RegisterProvider(LogProvider lp)
        {
            lock (providers)
                if (!providers.Contains(lp))
                    providers.Add(lp);
        }

        public void RegisterProvider(params LogProvider[] lp)
        {
            RegisterProviders(lp);
        }

        public void RegisterProviders(IEnumerable<LogProvider> providers)
        {
            foreach (var l in providers)
                RegisterProvider(l);
        }

        public void UnregisterProvider(LogProvider lp)
        {
            lock (providers)
                if (providers.Contains(lp))
                    providers.Remove(lp);
        }
        #endregion

        #region Logging Methods

        public void Log(object log, LogSeverity severity)
        {
            if (severity == LogSeverity.None)
                return;
            if (severity > SeverityLimit)
                return;
            lock (providers)
                foreach (var p in providers)
                    p.Log(log, severity);
        }

        public void Error(object log)
        {
            Log(log, LogSeverity.Error);
        }

        public void Warning(object log)
        {
            Log(log, LogSeverity.Warning);
        }

        public void Notice(object log)
        {
            Log(log, LogSeverity.Notice);
        }

        public void Verbose(object log)
        {
            Log(log, LogSeverity.Verbose);
        }
        #endregion

        #region Constructors

        public Logger()
        {
            RegisterProvider(new FileLogProvider("log.txt"));
        }

        public Logger(string path)
        {
            RegisterProvider(new FileLogProvider(path));
        }

        public Logger(params LogProvider[] providers)
        {
            RegisterProviders(providers);
        }
        #endregion

        #region Destructor
        public void Dispose()
        {
            lock (providers)
            {
                foreach (var p in providers)
                    p.Dispose();
                providers.Clear();
            }
        }
        #endregion
    }
    
}

