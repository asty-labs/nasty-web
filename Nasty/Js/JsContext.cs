using System;

namespace Nasty.Js
{
    /**
     * This class is a registry to collect javascript expressions
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class JsContext {

        [ThreadStatic]
	    private static JsSequence _contexts = new JsSequence();

        /**
         * This method is called by whoever wants to register a javascript call
         *
         * @param call call representation
         */
	    public static void Add(JsCall call) {
		    var fragment = _contexts;
		    if(fragment == null)
			    throw new Exception("no script context!");
		    fragment.Add(call);
	    }

        /**
         * This method provides context for all registrations within the specified delegate
         *
         * @param action    delegate, which may contain JsContext.add calls
         * @return          collected javascript expression
         */
	    public static IJsExpression Execute(Action action) {
		    var previous = _contexts;
		    try {
			    var fragment = new JsSequence();
			    _contexts = fragment;
			    action();
			    return fragment;
		    }
		    finally {
			    _contexts = previous;
		    }
	    }
    }
}