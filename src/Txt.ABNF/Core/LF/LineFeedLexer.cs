using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.LF
{
    public class LineFeedLexer : CompositeLexer<Terminal, LineFeed>
    {
        public LineFeedLexer([NotNull] ILexer<Terminal> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
