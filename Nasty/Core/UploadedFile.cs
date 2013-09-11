
using System.IO;

namespace Nasty.Core
{
    public interface IUploadedFile {

        string Name { get; }

        string OriginalFilename { get; }

        string ContentType { get; }

        bool IsEmpty { get; }

        int Size { get; }

        Stream InputStream { get; }
    }
}
