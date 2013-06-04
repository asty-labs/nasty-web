using System;
using System.Collections.Generic;
namespace Nasty.Core
{
    /**
     * This class is used to collect (register) all Components while rendering.
     * Used in FormEngine and in the component renderers (tags)
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class RenderingContext
    {

        private readonly Form _form;
        private readonly IList<Component> _components = new List<Component>();
        [ThreadStatic]
        private static RenderingContext _registries;

        public RenderingContext(Form form)
        {
            _form = form;
        }

        /**
         * Calls action in the thread-static context of this instance
         *
         * @param action action delegate
         */
        public void Collect(Action action)
        {
            var previous = _registries;
            try
            {
                _registries = this;
                action();
            }
            finally
            {
                _registries = previous;
            }
        }

        /**
         * Gets the current thread static instance
         *
         * @return rendering context
         */
        public static RenderingContext Instance
        {
            get
            {
                var reg = _registries;
                if (reg == null) throw new Exception("RenderingContext not set!");
                return reg;
            }
        }

        /**
         * Adds component to the rendering registry
         *
         * @param component
         */
        public void RegisterComponent(Component component)
        {
            _form.RegisterComponent(component);
            _components.Add(component);
        }

        public ICollection<Component> Components
        {
            get { return _components; }
        }
    }
}