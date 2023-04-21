using Avesta.Language;
using Avesta.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Exceptions
{
    //public class Lock
    //{
    //    static bool IsEnabled = false;

    //    public static void OnLock(Action action)
    //    {
    //        while (IsEnabled) { }
    //        IsEnabled = true;
    //        action();
    //        Release();
    //    }
    //    public static void Release() => IsEnabled = false;
    //}

    public class ErrorManager
    {
        public static void LogExceptionToFile(SystemException exception, LogSeverity severity = LogSeverity.Verbose)
        {
            var code = exception.Code;
            var stack = exception.StackTrace;
            var message = exception.Message;
            var now = DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss");

            var logMessage = $"[code : {code}] \n [message : {message}] \n stack trace : {stack}";

            LogManager.ApplicationLogger.Log(logMessage, severity);

        }


        public static void LogExceptionToFile(string exceptionMsg, string exceptionStack, LogSeverity severity = LogSeverity.Verbose)
        {
            var code = -1;
            var stack = (exceptionStack ?? "");
            var message = (exceptionMsg ?? "");
            var now = DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss");

            var logMessage = $"[code : {code}] \n [message : {message}] \n stack trace : {stack}";

            LogManager.ApplicationLogger.Log(logMessage, severity);

        }

        /// <summary>
        /// use pattern error.code.{code}
        /// </summary>
        /// <param name="code">code for message</param>
        /// <returns></returns>
        public static string GetErrorMessageByCode(int code)
        {
            throw new NotImplementedException();
        }

        
    }
}
