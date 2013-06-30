using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using Siska.Core;

namespace Siska.Service
{
    public class ServiceInterceptor : IInterceptor
    {
        public ILogger Logger { get; set; }

        public void Intercept(IInvocation invocation)
        {
            if (Logger.IsDebugEnabled) Logger.Debug(CreateInvocationLogString(invocation));
            try
            {
                if (invocation.Arguments != null && invocation.Arguments.Length > 0)
                {
                    ServiceRequest pars = invocation.Arguments[0] as ServiceRequest;

                    if (pars != null)
                    {
                        if (pars.NoParameters && pars.NoData)
                        {
                            invocation.Proceed();
                        }
                        else
                        {
                            if (!pars.NoParameters)
                            {
                                if (pars.Parameters == null)
                                {
                                    ServiceException exception = new ServiceException("Parameter is null");
                                    throw exception;
                                }
                            }
                            if (!pars.NoData)
                            {
                                if (pars.Data == null)
                                {
                                    ServiceException exception = new ServiceException("No Data");
                                    throw exception;
                                }
                            }

                            invocation.Proceed();
                        }
                    }
                    else
                    {
                        if (Logger.IsErrorEnabled) Logger.Error("Service Request have a different type");

                        ServiceException exception = new ServiceException("Service Request have a different type");
                        throw exception;
                    }
                }
                else
                {
                    if (Logger.IsErrorEnabled) Logger.Error("No Service Request Parameter");

                    ServiceException exception = new ServiceException("No Service Request Parameter");
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                if (Logger.IsErrorEnabled) Logger.Error(CreateInvocationLogString(invocation), ex);

                if (ex.GetType() != typeof(DaoException))
                {
                    ServiceException exception = new ServiceException(ex.Message, ex.InnerException);
                    throw exception;
                }
                else
                {
                    throw ex;
                }
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
