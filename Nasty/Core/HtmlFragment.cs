using System.Collections.Generic;
using Nasty.Js;
namespace Nasty.Core
{
    /**
     * This class keeps rendered HTML and corresponding javascript code (e.g. initialization)
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class HtmlFragment : IJsSerializable {

        public HtmlFragment(string html, JsClosure script) {
            Html = html;
            Script = script;
        }

        public string Html
        {
            get; private set;
        }

        public JsClosure Script
        {
            get; private set;
        }

        public IJsExpression ToJsExpression() {
            IDictionary<string, object> map = new Dictionary<string, object>();
            map.Add("html", Html);
            map.Add("script", Script);
            return new JsMap(map);
        }
    }
}