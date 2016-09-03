using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DIGIT
{
    public class DigitLexer : CompositeLexer<Terminal, Digit>
    {
        public DigitLexer([NotNull] ILexer<Terminal> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
