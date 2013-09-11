
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nasty.Core
{
    public class DefaultMethodInvoker : IMethodInvoker
    {
        private const string EventPrefix = "EVT.";

        public void Invoke(Form form, IParameterProvider parameterProvider)
        {
            var eventHandler = parameterProvider.GetParameter("eventHandler");
            var method = form.GetType().GetMethods().FirstOrDefault(x => x.Name == eventHandler);
            if (method == null) throw new MissingMethodException(GetType().FullName, eventHandler);
            var p = new List<object>();
            foreach (var parameter in method.GetParameters())
            {
                p.Add(ResolveParameterValue(parameter.Name, parameter.ParameterType, form, parameterProvider));
            }
            method.Invoke(form, p.ToArray());
        }

        protected virtual object ResolveParameterValue(string name, Type type, Form form,
                                                       IParameterProvider parameterProvider)
        {
            if (type == typeof (object) || type == typeof (EventArgs)) return ExtractEventArgs(form, parameterProvider);
            if (name.StartsWith("event"))
            {
                var parameterName = name.Substring("event".Length);
                parameterName = parameterName.Substring(0, 1).ToLower() + parameterName.Substring(1);
                var eventArgs = ExtractEventArgs(form, parameterProvider);
                var val = eventArgs[parameterName];
                if (typeof (ComponentProxy).IsAssignableFrom(type)) return form.Get(type, val);
                return ParseValue(type, val);
            }
            if (typeof(ComponentProxy).IsAssignableFrom(type)) return form.Get(type, name);
            if (type == typeof(IUploadedFile)) return parameterProvider.GetFile(form.GlobalizeId(name));
            var value = parameterProvider.GetParameter(form.GlobalizeId(name));
            if (value == null) return null;
            return ParseValue(type, value);
        }

        protected virtual object ParseValue(Type type, string value)
        {
            if (type == typeof (long)) return long.Parse(value);
            if (type == typeof (int)) return int.Parse(value);
            if (type == typeof (string)) return value;
            throw new Exception("cannot parse value " + value + " of type " + type.FullName);
        }

        /**
         * Fills EventArgs object from the request parameters
         *
         * @param map       request parameters
         * @param form      form, owning the event
         * @return          filled out EventArgs-object
         */

        private static EventArgs ExtractEventArgs(Form form, IParameterProvider data)
        {

            var args = new EventArgs();
            foreach (var key in data.ParameterNames)
            {
                if (key.StartsWith(EventPrefix))
                {
                    var value = data.GetParameter(key);
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