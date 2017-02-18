using System.IO;

namespace Txt.Core {
    public interface IBinaryDataSource {
        Stream GetStream();
    }
}