using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using Siska.Core;

namespace Siska.Data
{
    public class DaoInterceptor : IInterceptor
    {
        public ILogger Logger { get; set; }

        public void Intercept(IInvocation invocation)
        {
            if (Logger.IsDebugEnabled) Logger.Debug(CreateInvocationLogString(invocation));
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                if (Logger.IsErrorEnabled) Logger.Error(CreateInvocationLogString(invocation), ex);

                DaoException exception = new DaoException(ex.Message, ex.InnerException);
                throw exception;
            }
        }

        public static String CreateInvocationLogString(IInvocation invocation)
        {
            StringBuilder sb = new StringBuilder(100);
            sb.AppendFormat("Called: {0}.{1}(", invocation.TargetType.Name, invocation.Method.Name);
            foreach (object argument in invocation.Arguments)
            {
                String argumentDescription = argument == null ? "null" : argument.ToString();
                sb.Append(argumentDescription).Append(",");
            }
            if (invocation.Arguments.Count() > 0) sb.Length--;
            sb.Append(")");
            return sb.ToString();
        }
    }
}
