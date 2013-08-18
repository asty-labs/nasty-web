using System;
using System.Reflection;
using Nasty.Js;

namespace Nasty.Core
{
    public class SimpleErrorHandler : IMethodInvoker
    {
        private readonly IMethodInvoker _methodInvoker;

        public SimpleErrorHandler(IMethodInvoker methodInvoker)
        {
            _methodInvoker = methodInvoker;
        }

        public void Invoke(Form form, IParameterProvider parameterProvider)
        {
            try
            {
                _methodInvoker.Invoke(form, parameterProvider);
            }
            catch (TargetInvocationException e)
            {
                Handle(e.InnerException);
            }
            catch (Exception t)
            {
                Handle(t);
            }
        }

        static void Handle(Exception e)
        {
            JsContext.Add(new JsCall("alert", e.Message));
        }

    }
}
