using Nasty.Core;
namespace Nasty.Components
{
    /**
     * Component proxy for generic JQuery-functions
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class JQuery : ComponentProxy {

	    public JQuery Html(object value) {
		    Invoke("html", value);
		    return this;
	    }

	    public JQuery Text(string value) {
		    Invoke("text", value);
		    return this;
	    }
	
	    public JQuery Append(object content) {
		    Invoke("append", content);
		    return this;
	    }
	
	    public JQuery Empty() {
		    Invoke("empty");
		    return this;
	    }
    }
}