using System.Collections.Generic;
using System.Linq;
using Nasty.Core;

namespace Nasty.Components
{
    public class FileUpload : Component
    {
        public override void Restore(IParameterProvider data)
        {
            if (data.ParameterNames.Contains(Id))
                File = data.GetFile(Id);
        }

        public IUploadedFile File { get; private set; }

        public override string HtmlTag
        {
            get { return "input"; }
        }

        protected override void FillHtmlAttributes(IDictionary<string, string> attributes)
        {
            base.FillHtmlAttributes(attributes);
            attributes.Add("type", "file");
        }
    }
}
