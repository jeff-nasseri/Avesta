using Avesta.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Logging
{
    public abstract class LogProvider : IDisposable
    {
        #region Methods
        public abstract void Log(object log, LogSeverity severity);

        public void Log(object log)
        {
            Log(log, LogSeverity.None);
        }
        #endregion

        #region Dispose
        public virtual void Dispose()
        {
        }
        #endregion
    }
}
