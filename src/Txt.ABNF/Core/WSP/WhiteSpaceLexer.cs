using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.WSP
{
    public class WhiteSpaceLexer : CompositeLexer<Alternation, WhiteSpace>
    {
        public WhiteSpaceLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
