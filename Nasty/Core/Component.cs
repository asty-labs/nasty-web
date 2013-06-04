using Nasty.Js;
using System.Collections.Generic;
using Nasty.Utils;
using System;
namespace Nasty.Core
{
    /**
     * This is the base class for all components, requiring initialisation
     * (having the init-method).
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    [Serializable]
    public abstract class Component : ComponentProxy, IJsSerializable {

        /**
         * Custom data, associated with the component instance
         */
        private bool _visible = true;

        protected Component(bool voidTag)
        {
            VoidTag = voidTag;
        }

        protected Component()
            : this(false)
        {
        }

        public bool VoidTag
        {
            get;
            private set;
        }

        [InitProperty]
        public string Data
        {
            get; set;
        }

        public virtual string HtmlTag {
            get { return "div"; }
        }

        public IDictionary<string, string> GetHtmlAttributes() {
            var attributes = new Dictionary<string, string> {{"id", ClientId}};
            FillHtmlAttributes(attributes);
            return attributes;
        }
    
        protected virtual void FillHtmlAttributes(IDictionary<string, string> attributes) {

        }

	    private IDictionary<string, object> GetInitialOptions() {
		    var opts = new Dictionary<string, object>();
		    FillInitialOptions(opts);
		    return opts;
	    }

        /**
         * Default implementation collects all fields, marked with @InitProperty
         *
         * @param opts init parameter to be filled by the method
         */
	    protected virtual void FillInitialOptions(IDictionary<string, object> opts) {
            opts.Add("id", Id);
		    foreach(var f in ReflectionUtils.GetAllInitProperties(GetType(), typeof(InitPropertyAttribute))) {
				object value = f.GetValue(this, new object[] {});
                if(value != null)
				    opts.Add(f.Name.Substring(0, 1).ToLower() + f.Name.Substring(1), value);
		    }
	    }
	
	    public void Init() {
		    Invoke("init", GetInitialOptions());
	    }

        public IJsExpression ToJsExpression() {
            var component = this;
            var script = new JsClosure(component.Init);
            return new HtmlFragment(ComponentRenderingHelper.GetStartTag(this) + ComponentRenderingHelper.GetEndTag(this),
                    script).ToJsExpression();
        }

        [InitProperty]
        public bool Visible {
            get { return _visible; }
            set
            {
                _visible = value;
                Invoke("visible", value);
            }
        }
    }
}