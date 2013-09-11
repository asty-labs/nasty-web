using System;
using System.Collections.Generic;
using Nasty.Js;

namespace Nasty.Core
{

    /**
     * This is the base class for all javascript component server-proxies.
     * The target component(s) is addressed by id or query
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    [Serializable]
    public abstract class ComponentProxy {

        /**
         * To address component by id
         */
        public string Id { get; set; }
        /**
         * To address component(s) by jQuery selector
         */
        [NonSerialized]
        private string _query;

        public string ClientId {
            get { return Id.Replace('.', '_'); }
        }

        public string Query {
            set { _query = value; }
        }

        /**
         * jQuery-component to dispatch proxy calls to the specific javascript
         * components. Dispatcher components is aware of the namespace and
         * of the call syntax for the javascript methods of this component family.
         *
         * @return name of the jQuery component
         */
        protected virtual string Family {
            get { return "jasty"; }
        }

        /**
         * Name of the proxied javascript component.
         * Default convention: javascript component name is the proxy class name
         *
         * @return component name
         */
        protected virtual string ClassName {
            get { return GetType().Name; }
        }

        /**
         * This method generates corresponding javascript call of the proxied component
         * and registers it in the current scripting context.
         *
         * @param method method name
         * @param params arbitrary method parameters
         */
        protected void Invoke(string method, params object[] parameters) {
            if(Id == null && _query == null) return;
            var p = new List<object>();
            p.AddRange(parameters);
            String selector = Id == null ? "$('" + _query + "').": "$$('" + ClientId + "').";
            JsContext.Add(new JsCall(selector + Family, ClassName, method, p));
        }

        public void AddClass(string value) {
            Invoke("addClass", value);
        }

        public void RemoveClass(string value) {
            Invoke("removeClass", value);
        }

        public void Remove() {
            Invoke("remove");
        }

        /**
         * This method can be overridden to restore the internal state of the component
         * from the current request.
         *
         * @param data all available data to restore component state from
         */
        public virtual void Restore(IParameterProvider data) {

        }
    }
}