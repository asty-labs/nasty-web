namespace Nasty.Js
{

    /**
     * This class represents string constants
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class JsString : IJsExpression {

	    private readonly string _value;
	
	    public JsString(string value) {
		    _value = value;
	    }
	
	    public string Encode() {
		    return "\"" + _value.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r") + "\"";
	    }
    }
}