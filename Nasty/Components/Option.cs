using Nasty.Js;
namespace Nasty.Components
{
    /**
     * Component proxy for HTML combobox option
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class Option : IJsSerializable {

        public Option(string value, string text) {
            Value = value;
            Text = text;
        }

        public string Value
        {
            get; set;
        }

        public string Text
        {
            get; set;
        }

        public IJsExpression ToJsExpression() {
            return JsList.From(Value, Text);
        }
    }
}