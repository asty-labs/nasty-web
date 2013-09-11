using System.Collections.Generic;
namespace Nasty.Core
{
    /**
     * This interface is an abstraction to get request parameters. Used by FormEngine
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public interface IParameterProvider
    {

        string GetParameter(string name);

        IUploadedFile GetFile(string name);

        IEnumerable<string> ParameterNames { get; }

        string[] GetParameterValues(string name);

    }
}