using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CTL
{
    public class ControlCharacterLexer : CompositeLexer<Alternation, ControlCharacter>
    {
        public ControlCharacterLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
