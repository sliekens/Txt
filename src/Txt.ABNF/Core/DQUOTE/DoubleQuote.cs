using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

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
