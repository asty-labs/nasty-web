using System.Collections.Generic;

namespace Nasty.Core
{
    public class DefaultMethodInvoker : IMethodInvoker
    {
        private const string EventPrefix = "EVT.";

        public void Invoke(Form form, IParameterProvider parameterProvider)
        {
            var eventHandler = parameterProvider.GetParameter("eventHandler");
            var args = ExtractEventArgs(parameterProvider.GetParameterMap(), form);
            var method = form.GetType().GetMethod(eventHandler, new[] { typeof(EventArgs) });
            method.Invoke(form, new object[] { args });
        }

        /**
         * Fills EventArgs object from the request parameters
         *
         * @param map       request parameters
         * @param form      form, owning the event
         * @return          filled out EventArgs-object
         */
        private static EventArgs ExtractEventArgs(IDictionary<string, string[]> map, Form form)
        {

            var args = new EventArgs();
            foreach (var key in map.Keys)
            {
                if (key.StartsWith(EventPrefix))
                {
                    var value = map[key][0];
                    var newKey = key.Substring(EventPrefix.Length);
                    if ("srcId".Equals(newKey))
                        args.SrcId = value.Substring(form.Id.Length + 1);
                    else
                        args[newKey] = value;
                }
            }
            return args;
        }

    }
}
