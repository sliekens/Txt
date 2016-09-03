using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.HEXDIG
{
    public class HexadecimalDigitLexer : CompositeLexer<Alternation, HexadecimalDigit>
    {
        public HexadecimalDigitLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
