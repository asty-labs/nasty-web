using System.Collections.Generic;
using System.Linq;
using Nasty.Core;
namespace Nasty.Components
{
    /**
     * Component proxy for HTML text editor
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class TextBox : Component {

	    private string _value;

        public TextBox()
            : base(true)
        {
        }

        [InitProperty]
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Invoke("value", value);
            }
	    }

        public override void Restore(IParameterProvider data)
        {
            if(data.ParameterNames.Contains(Id))
    		    _value = data.GetParameter(Id);
	    }

        public override string HtmlTag {
            get { return "input"; }
        }

        protected override void FillHtmlAttributes(IDictionary<string, string> attributes) {
            attributes.Add("type", "text");
        }
    }
}