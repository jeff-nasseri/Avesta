using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Logging
{
    public class LogManager
    {
        public static Logger ApplicationLogger = new Logger(new FileLogProvider("application_log.txt"));
    }
}
