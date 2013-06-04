using System.Collections;
using System.Collections.Generic;
namespace Nasty.Js
{
    /**
     * Factory to wrap any object in the appropriate javascript expression
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class JsExpressionFactory
    {

        public static IJsExpression create(object value) {
		    if(value == null)
			    return new JsVariable("null");
            if(value is IJsSerializable)
                return ((IJsSerializable)value).ToJsExpression();
            if(value is string)
                return new JsString((string)value);
		    if(value is IDictionary<string, object>)
			    return new JsMap((IDictionary<string, object>)value);
            if(value is IEnumerable<object>)
                return new JsList((IEnumerable<object>)value);
		    if(value is IJsExpression)
			    return (IJsExpression)value;
		    if(value is int)
			    return new JsNumber((int)value);
		    if(value is long)
			    return new JsNumber((long)value);
		    if(value is byte)
			    return new JsNumber((byte)value);
		    if(value is short)
			    return new JsNumber((short)value);
		    if(value is float)
			    return new JsNumber((float)value);
		    if(value is double)
			    return new JsNumber((double)value);
		    if(value is decimal)
			    return new JsNumber((decimal)value);
            if(value is bool)
                return new JsBoolean((bool)value);
		    if(value is char)
			    return new JsString(value.ToString());
		    return new JsString(value.ToString());
	    }
    }
}