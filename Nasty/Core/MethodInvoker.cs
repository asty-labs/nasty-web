
namespace Nasty.Core
{
    public interface IMethodInvoker
    {
        void Invoke(Form form, IParameterProvider parameterProvider);
    }
}
