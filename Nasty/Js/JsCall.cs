using System.Text;
namespace Nasty.Js
{
    /**
     * This class represents call of a function with parameters
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class JsCall : IJsExpression {

	    private readonly object[] _args;
	    private readonly string _method;
	
	    public JsCall(string method, params object[] args) {
		    _method = method;
		    _args = args;
	    }
	
	    public string Encode() {
		    var builder = new StringBuilder(_method);
		    builder.Append("(");
		    var argumentProcessed = false;
		    foreach(var param in _args) {
			    if(argumentProcessed)
				    builder.Append(", ");
			    else
				    argumentProcessed = true;
			    var expr = JsExpressionFactory.create(param);
			    builder.Append(expr.Encode());
		    }
		    builder.Append(")");
		    return builder.ToString();
	    }
    }
}