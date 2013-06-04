using System.Text;
namespace Nasty.Core
{
    /**
     * This class contains helper methods to generate initial HTML-code for
     * javascript components.
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class ComponentRenderingHelper
    {

        public static string GetStartTag(Component component)
        {
            var sb = new StringBuilder("<").Append(component.HtmlTag);
            AppendAttributes(sb, component);
            sb.Append(">");
            return sb.ToString();
        }

        public static string GetEndTag(Component component)
        {
            return new StringBuilder("</").Append(component.HtmlTag).Append(">").ToString();
        }

        public static string GetVoidTag(Component component)
        {
            var sb = new StringBuilder("<").Append(component.HtmlTag);
            AppendAttributes(sb, component);
            sb.Append("/>");
            return sb.ToString();
        }

        private static void AppendAttributes(StringBuilder sb, Component component) {
            foreach(var entry in component.GetHtmlAttributes()) {
                sb.Append(" ").Append(entry.Key).Append("=\"").Append(entry.Value).Append("\"");
            }
        }
    }
}