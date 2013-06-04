using System.Collections.Generic;
using System.Text;

namespace Nasty.Js
{
    /**
     * This class represents maps
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class JsMap : IJsExpression {

	    private IDictionary<string, object> _map = new Dictionary<string, object>();
	
	    public JsMap() {
		
	    }
	
	    public JsMap(IDictionary<string, object> map) {
            foreach(var set in map) 
		        _map.Add(set.Key, set.Value);
	    }
	
	    public string Encode() {
		    var builder = new StringBuilder("{");
		    var hasFirst = false;
		    foreach(var key in _map.Keys) {
			    if(hasFirst)
				    builder.Append(", ");
			    else
				    hasFirst = true;
			    builder.Append(JsExpressionFactory.create(key).Encode());
			    builder.Append(":");
			    builder.Append(JsExpressionFactory.create(_map[key]).Encode());
		    }
		    builder.Append("}");
		    return builder.ToString();
	    }
    }
}