using System;
using System.Reflection;
using Nasty.Core;
using Nasty.Js;

namespace Nasty.Mvc
{
    class SimpleErrorHandlingLink : AbstractProcessingLink
    {
        public override IJsExpression DoProcess()
        {
            try
            {
                return base.DoProcess();
            }
            catch (TargetInvocationException e)
            {
                return Handle(e.InnerException);
            }
            catch (Exception t)
            {
                return Handle(t);
            }
        }

        static IJsExpression Handle(Exception e)
        {
            return new JsCall("alert", e.Message);
        }
    }
}
