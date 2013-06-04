using System;
namespace Nasty.Core
{
    /**
     * This is the base class for all Forms. Forms are stateful presenters/controllers with event handler
     * methods, where FormEngine dispatches requests to.
     *
     * Form objects are serializable, so the fields that shouldn't or can't be serialized,
     * must be marked as transient.
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    [Serializable]
    public class Form : Component {

        /**
         * When a component gets registered in Form and has no id, Form assigns it a generated one.
         * For subsequent requests the last used id should be tracked, to avoid name clashes.
         *
         */
	    private int _lastAssignedChildId;

        /**
         * FormEngine is injected on every request
         */
        [NonSerialized]
	    private FormEngine _formEngine;

        /**
         * This flag is set, if the form is disposed (e.g. replaced by another one) and doesn't
         * need to be persisted.
         *
         */
        [NonSerialized]
        private bool _disposed;

        public override string HtmlTag {
            get { return "form"; }
        }

        /**
         * While rendering form view, every rendered component should be registered in the
         * Form-object via this method. The main purpose is to adjust component id, to make it
         * unique.
         *
         * @param component  to be registered
         */
        public void RegisterComponent(Component component) {
            component.Id = GlobalizeId(component.Id);
        }

        /**
         * Processes id to ensure it's uniqueness among different Forms by prepending it
         * with the Form-id
         *
         * @param initialId to be adjusted
         * @return unique id
         */
	    private string GlobalizeId(string initialId) {
		    if(initialId == null)
			    initialId = "c" + _lastAssignedChildId++;
		    return ClientId + "." + initialId;
	    }
	
	    public FormEngine FormEngine {
            set { _formEngine = value; }
	    }

        /**
         * This method creates component proxy for the given type and id. The id is automatically
         * globalized and the restore-method is called on the component to read the state from
         * the request.
         *
         * @param type  component proxy class to be returned
         * @param id    component id within the Form
         * @param <T>   component type
         * @return      instance of the component proxy, with restored state
         */
	    protected T Get<T>(string id) where T : ComponentProxy, new() {
			var obj = new T {Id = GlobalizeId(id)};
	        obj.Restore(_formEngine.GetParameterMap());
			return obj;
	    }

        /**
         * This method creates component proxy for the given type and selector. The ids
         * in the selector are automatically globalized.
         *
         * @param type  component proxy class to be returned
         * @param query selector
         * @param <T>   component type
         * @return      instance of the component proxy, with restored state
         */
	    protected new T Query<T>(string query) where T : ComponentProxy, new() {
			var obj = new T {Query = query.Replace("#", "#" + ClientId + "_")};
	        return obj;
	    }

        /**
         * This method can be used in the event handlers of the Form to get request parameters.
         *
         * @param name  local parameter name
         * @return      value of the parameter
         *
         */
	    protected string GetParameter(string name) {
		    return _formEngine.GetParameter(GlobalizeId(name));
	    }

        /**
         * This method can be used in the event handlers of the Form for rendering view fragments.
         *
         * @param fragmentName  fragment name to be rendered
         * @param model         model to be applied to the view
         * @return              HtmlFragment with rendered fragment
         *
         */
	    public HtmlFragment RenderFragment(string fragmentName, object model) {
		    return _formEngine.RenderFragment(this, fragmentName, model);
	    }

        /**
         * Renders the specified Form and replaces with it the current one
         *
         * @param form  Form to overwrite the current one
         */
        protected void ReplaceWith(Form form) {
            form.Id = Id;
            Invoke("replaceWith", _formEngine.RenderMainView(form));
            _disposed = true;
        }

        /**
         * This method can be overridden to provide model object for the Form's main view
         *
         * @return  model object for the main view
         *
         */
	    public virtual object PrepareModel() {
		    return null;
	    }

        /**
         * This method is called after all components of the Form's main view are initialized. Typically,
         * you would override it to additionally manipulate some components, after the standard
         * initialization of the main view is done.
         *
         */
	    public virtual void AfterInit() {
	    }

        /**
         * The javascript component is fixed to "Form", because we are not going to have
         * javascript component for every inherited Form-class
         *
         * @return  javascript component name
         */
	    protected override string ClassName {
            get { return "Form"; }
	    }

        /**
         * Sends Form's state to the client
         *
         * @param state state representation for the client
         *
         */
	    public void Update(string state) {
            if(_disposed) return;
		    Invoke("update", state);
	    }
    }
}