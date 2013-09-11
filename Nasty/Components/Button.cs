using Nasty.Core;
using System.Collections.Generic;
namespace Nasty.Components
{
    /**
     * Component proxy for HTML Button
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class Button : Component {
	
	    private string _text;

        public Button()
            : base(true)
        {
        }

        [InitProperty]
        public string OnClick { get; set; }

        [InitProperty]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                Invoke("text", value);
            }
	    }

        public override string HtmlTag {
            get { return "button"; }
        }
    }
}