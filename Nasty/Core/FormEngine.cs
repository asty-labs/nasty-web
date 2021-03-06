using System;
using System.Collections.Generic;
using System.Text;
using Nasty.Js;

namespace Nasty.Core
{
    /**
     * This is the central class for managing Form events, rendering views and
     * resolving entry points
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class FormEngine
    {

        private readonly IParameterProvider _parameterProvider;
        private readonly IViewRenderer _viewRenderer;
        private readonly IFormPersister _formPersister;
        private readonly IMethodInvoker _methodInvoker;

        public FormEngine(IParameterProvider parameterProvider,
                          IViewRenderer viewRenderer, IFormPersister formPersister, IMethodInvoker methodInvoker) {

            _parameterProvider = parameterProvider;
            _viewRenderer = viewRenderer;
            _formPersister = formPersister;
            _methodInvoker = methodInvoker;
        }

        /**
         * Resolves Form-object from the entry point class name and parameters.
         * The entry point class must have parameterless constructor and
         * either implement FormFactory or be a Form.
         *
         * @param entryPointClass
         * @param parameters
         * @return resolved form
         *
         */
        public static Form CreateInitialForm(string entryPointClass, string parameters)
        {
            var type = Type.GetType(entryPointClass);
            if(type == null)
                throw new Exception("Cannot find class: " + entryPointClass);

            var constructor = type.GetConstructor(new Type[] {});
            if(constructor == null)
                throw new Exception("Class doesn't have parameterless constructor: " + entryPointClass);
            var obj = constructor.Invoke(new object[] { });
            var factory = obj as IFormFactory;
            if(factory != null) return factory.CreateForm(parameters);
            var form = obj as Form;
            if(form != null) return form;
            throw new Exception("Wrong class: must be either FormFactory or Form");
        }
    
        public virtual IParameterProvider ParameterProvider {
            get { return _parameterProvider; }
        }

        /**
         * Renders main view for the Form, using the model returned by prepareModel-method.
         * Generates javascript to initialize all rendered components.
         *
         * @param form  Form to be rendered
         * @return      HtmlFragment with rendered HTML and initialization script
         */
        public HtmlFragment RenderMainView(Form form) {
            form.FormEngine = this;
            var builder = new StringBuilder();
            builder.Append(ComponentRenderingHelper.GetStartTag(form));
            var model = form.PrepareModel();
            var reg = new RenderingContext(form);
            reg.Collect(() => builder.Append(_viewRenderer.RenderMainView(form, model)));
            builder.Append(ComponentRenderingHelper.GetEndTag(form));
            var script = new JsClosure(delegate
                {
                    form.Init();
                    foreach (var comp in reg.Components)
                    {
                        comp.Init();
                    }
                    form.AfterInit();
                    UpdateForm(form);
                });
            return new HtmlFragment(builder.ToString(), script);
        }

        /**
         * Renders view fragment for the Form, using the specified model.
         * Generates javascript to initialize all rendered components.
         *
         * @param form      Form of the fragment to be rendered
         * @param fragment  fragment name
         * @param model     model for the fragment view
         * @return      HtmlFragment with rendered HTML and initialization script
         */
        public HtmlFragment RenderFragment(Form form, string fragment, object model) {
            form.FormEngine = this;
            var reg = new RenderingContext(form);
            var builder = new StringBuilder();
            reg.Collect(() => builder.Append(_viewRenderer.RenderFragment(form, fragment, model)));
            var script = new JsClosure(() => {
                    foreach (var comp in reg.Components) {
                        comp.Init();
                    }
            });
            return new HtmlFragment(builder.ToString(), script);
        }

        /**
         * This method dispatches event to the handler method of the appropriate Form. Dispatching
         * data are obtained from the parameter provider.
         *
         * @param errorHandler  delegate to handle unprocessed exceptions
         * @return              JsExpression to be sent as the response
         */
        public IJsExpression DoProcess() {
            return JsContext.Execute(ProcessEvent);
        }

        private void ProcessEvent() {
            var form = _formPersister.Lookup(_parameterProvider.GetParameter("state"));
            form.FormEngine = this;
            _methodInvoker.Invoke(form, _parameterProvider);
            UpdateForm(form);
        }

        private void UpdateForm(Form form) {
            form.Update(_formPersister.Persist(form));
        }
    }
}