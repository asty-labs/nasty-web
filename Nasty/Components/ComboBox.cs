using System.Collections.Generic;
using System.Linq;
using Nasty.Core;
namespace Nasty.Components
{
    /**
     * Component proxy for HTML combo editor
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class ComboBox : Component {
        
        private string _value;

        [InitProperty]
        public string OnChange { get; set; }

        private IEnumerable<Option> _options;

        public override void Restore(IParameterProvider data) {
            if(data.ParameterNames.Contains(Id))
                _value = data.GetParameter(Id);
        }

        public override string HtmlTag {
            get { return "select"; }
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

        [InitProperty]
        public IEnumerable<Option> Options
        {
            get { return _options; }
            set {
                _options = value;
                Invoke("options", _options);
            }
        }
    }
}