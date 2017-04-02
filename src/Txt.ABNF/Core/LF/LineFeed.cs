using JetBrains.Annotations;

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
