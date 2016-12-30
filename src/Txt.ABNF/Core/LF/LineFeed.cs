using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.LF
{
    public class LineFeed : Terminal
    {
        public LineFeed([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
