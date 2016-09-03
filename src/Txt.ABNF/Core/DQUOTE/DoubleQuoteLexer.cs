using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DQUOTE
{
    public class DoubleQuoteLexer : CompositeLexer<Terminal, DoubleQuote>
    {
        public DoubleQuoteLexer([NotNull] ILexer<Terminal> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
