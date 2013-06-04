namespace Nasty.Core
{
    public interface IFormPersister
    {

        Form Lookup(string key);

        string Persist(Form form);
    }
}