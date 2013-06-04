namespace Nasty.Core
{
    /**
     * This interface is an abstraction to render main views or view fragments
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public interface IViewRenderer
    {

        string RenderMainView(Form form, object model);

        string RenderFragment(Form form, string fragmentName, object model);

    }
}