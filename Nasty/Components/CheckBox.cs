using System.Collections.Generic;
using System.Linq;
using Nasty.Core;
namespace Nasty.Components
{
    /**
     * Component proxy for HTML checkbox editor
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class CheckBox : Component {

        private bool _checked;

        public CheckBox()
            : base(true)
        {
        }

        [InitProperty]
        private string OnChange { get; set; }

        public override void Restore(IParameterProvider data) {
            _checked = data.ParameterNames.Contains(Id) && "1".Equals(data.GetParameter(Id));
        }

        public override string HtmlTag {
            get { return "input"; }
        }

        [InitProperty]
        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                Invoke("checked", value);
            }
        }

        protected override void FillHtmlAttributes(IDictionary<string, string> attributes) {
            attributes.Add("type", "checkbox");
            attributes.Add("value", "1");
        }
    }
}