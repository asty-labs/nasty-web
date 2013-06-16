using Nasty.Js;

namespace Nasty.Core
{
    public interface IProcessingLink
    {
        IJsExpression DoProcess();
    }

    public abstract class AbstractProcessingLink : IProcessingLink
    {
        private IProcessingLink _next;

        public IProcessingLink Next { set { _next = value; } }

        public virtual IJsExpression DoProcess()
        {
            return _next != null ? _next.DoProcess() : null;
        }
    }
}
