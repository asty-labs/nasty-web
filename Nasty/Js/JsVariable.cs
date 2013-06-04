namespace Nasty.Js
{
    /**
     * This class represents variable names
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class JsVariable : IJsExpression {

	    private readonly string _name;
	
	    public JsVariable(string name) {
		    _name = name;
	    }

	    public string Encode() {
		    return _name;
	    }
    }
}