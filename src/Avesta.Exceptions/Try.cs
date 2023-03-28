using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Exceptions
{
    public class Try
    {

        public static Action Catch(Action tryAction, Action<Exception>? catchAction = null)
              => Catch<Exception>(tryAction, catchAction);


        public static Action Catch<TException>(Action tryAction, Action<TException>? catchAction = null)
            where TException : Exception
            => Catch<TException>(tryAction, catchAction);


        public static Action Catch<TException>(Action tryAction, Action<TException>? catchAction = null, Action? final = null)
            where TException : Exception
            => delegate
            {
                try
                {
                    tryAction();
                }
                catch (TException exception)
                {
                    catchAction?.Invoke(exception);
                }
                finally
                {
                    final?.Invoke();
                }
            };
    }

}
