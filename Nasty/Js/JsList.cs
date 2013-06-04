using System.Collections.Generic;
using System.Text;

namespace Nasty.Js
{
    /**
     * This class represents lists or arrays
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class JsList : IJsExpression {

	    private readonly List<object> _items = new List<object>();
	
	    public JsList() {
	    }
	
	    public JsList(IEnumerable<object> items) {
            foreach(var o in items) _items.Add(o);
	    }
	
	    public JsList(object[] items) {
            _items.AddRange(items);
	    }

	    public static JsList From(params object[] parameters) {
		    var result = new JsList();
            result._items.AddRange(parameters);
		    return result;
	    }
	
	    public string Encode() {
		    var builder = new StringBuilder("[");
		    var hasFirst = false;
		    foreach(var item in _items) {
			    if(hasFirst)
				    builder.Append(", ");
			    else
				    hasFirst = true;
			    builder.Append(JsExpressionFactory.create(item).Encode());
		    }
		    builder.Append("]");
		    return builder.ToString();
	    }
    }
}