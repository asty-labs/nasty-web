using System.Collections.Generic;
using System;
using System.Text;

namespace Nasty.Js
{
    /**
    * This class represents closure (anonymous function)
    *
    * @author Stanislav Tkachev
    * @version 1.0
    *
    */
    public class JsClosure : IJsExpression {

        private readonly IJsExpression _body;
        private readonly List<JsVariable> _args = new List<JsVariable>();

        /**
            * This constructor uses JsExpression as closure body directly
            *
            * @param body  code of the closure as JsExpression
            * @param args  closure parameters
            */
        public JsClosure(IJsExpression body, params JsVariable[] args) {
	        _body = body;
            _args.AddRange(args);
        }

        /**
            * This constructor collects JsExpression by running delegate
            *
            * @param closure  delegate to be run to collect JsExpression
            * @param args  closure parameters
            */
        public JsClosure(Action closure, params JsVariable[] args):
            this(JsContext.Execute(closure), args)
        {
        }
	
        public string Encode() {
	        var builder = new StringBuilder();
	        builder.Append("function(");
	        var hasFirst = false;
	        foreach(var var in _args) {
		        if(hasFirst)
			        builder.Append(", ");
		        else
			        hasFirst = true;
		        builder.Append(var.Encode());
	        }
	        builder.Append("){").Append(_body.Encode()).Append("}");
	        return builder.ToString();
        }

        public string EncodeOnLoadAsHtml() {
            return "<script>jQuery(" + Encode() + ")</script>";
        }
    }
}