using System.Globalization;
namespace Nasty.Js
{
    /**
     * This class represents numeric constants
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class JsNumber : IJsExpression {

	    private readonly string _text;

	    public JsNumber(decimal value) {
		    _text = value.ToString("G", new CultureInfo("").NumberFormat);
	    }

	    public JsNumber(double value) {
            _text = value.ToString(new CultureInfo("").NumberFormat);
	    }

	    public JsNumber(long value) {
		    _text = value.ToString();
	    }

	    public string Encode() {
		    return _text;
	    }
    }
}