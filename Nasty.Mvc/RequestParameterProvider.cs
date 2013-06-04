using Nasty.Core;
using System.Collections.Generic;
using System.Web;
namespace Nasty.Mvc
{
     /**
     * Facade to the parameters from ServletRequest
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class RequestParameterProvider : IParameterProvider {

        private readonly HttpRequest _req;

        public RequestParameterProvider(HttpRequest req) {
            _req = req;
        }

        public string GetParameter(string name) {
            return _req.Params.Get(name);
        }

        public IDictionary<string, string[]> GetParameterMap() {
            var result = new Dictionary<string, string[]>();
            foreach (var key in _req.Params.AllKeys)
            {
                result.Add(key, _req.Params.GetValues(key));
            }
            return result;
        }
    }
}