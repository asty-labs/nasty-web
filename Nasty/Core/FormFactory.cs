namespace Nasty.Core
{
    /**
     * This interface should be implemented by the Form-factory. Form-factories
     * can be used to instantiate initial Form on the conventional entry-point pages.
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public interface IFormFactory
    {

        Form CreateForm(string parameters);
    }
}