using JetBrains.Annotations;

namespace Txt.ABNF.Core.DQUOTE
{
    public class DoubleQuote : Terminal
    {
        public DoubleQuote([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
