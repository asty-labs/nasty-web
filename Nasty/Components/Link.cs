using Nasty.Core;
namespace Nasty.Components
{
    /**
     * Component proxy for HTML link (anchor)
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class Link : Component {
	
	    private string _text;
        [InitProperty]
        public string OnClick { get; set; }

	    public string Text {
            get { return _text; }
            set
            {
                _text = value;
                Invoke("text", value);
            }
	    }
    
        public override string HtmlTag {
            get { return "a"; }
        }
    }
}