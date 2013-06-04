using System.Collections.Generic;
using System.Text;

namespace Nasty.Js
{
    /**
     * This class represents sequence of statements, separated by semicolon
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class JsSequence : IJsExpression {

	    private readonly ICollection<IJsExpression> _expressions = new List<IJsExpression>();

	    public JsSequence(params IJsExpression[] expressions) {
		    foreach(var expr in expressions) {
			    Add(expr);
		    }
	    }
	
	    public void Add(IJsExpression expr) {
		    _expressions.Add(expr);
	    }
	
	    public string Encode() {
		    var builder = new StringBuilder();
		    foreach(var expr in _expressions) {
			    builder.Append(expr.Encode());
			    builder.Append(";");
		    }
		    return builder.ToString();
	    }
    }
}