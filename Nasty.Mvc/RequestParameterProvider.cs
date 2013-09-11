using System.IO;
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
            return _req.Form.Get(name);
        }

        public IUploadedFile GetFile(string name)
        {
            return new UploadedFile(_req.Files[name], name);
        }

        public IEnumerable<string> ParameterNames 
        {
            get
            {
                var result = new List<string>(_req.Form.AllKeys);
                result.AddRange(_req.Files.AllKeys);
                return result;
            }
        }

        public string[] GetParameterValues(string name)
        {
            var result = _req.Form.GetValues(name);
            return result ?? new string[0];
        }

        class UploadedFile : IUploadedFile
        {
            private readonly HttpPostedFile _file;
            private readonly string _name;

            public UploadedFile(HttpPostedFile file, string name)
            {
                _file = file;
                _name = name;
            }

            public string Name
            {
                get { return _name; }
            }

            public string OriginalFilename
            {
                get { return _file.FileName; }
            }

            public string ContentType
            {
                get { return _file.ContentType; }
            }

            public bool IsEmpty
            {
                get { return _file == null || string.IsNullOrEmpty(OriginalFilename); }
            }

            public int Size
            {
                get { return _file.ContentLength; }
            }

            public Stream InputStream
            {
                get { return _file.InputStream; }
            }
        }
    }
}