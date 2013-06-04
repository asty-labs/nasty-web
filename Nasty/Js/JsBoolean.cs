namespace Nasty.Js
{
    /**
     * This class represents boolean literals
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class JsBoolean : IJsExpression{

	    private readonly bool _value;
	
	    public JsBoolean(bool value) {
		    _value = value;
	    }
	
	    public string Encode() {
		    return _value ? "true" : "false";
	    }
    }
}