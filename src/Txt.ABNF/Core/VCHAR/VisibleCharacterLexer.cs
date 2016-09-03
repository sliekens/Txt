using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.VCHAR
{
    public class VisibleCharacterLexer : CompositeLexer<Terminal, VisibleCharacter>
    {
        public VisibleCharacterLexer([NotNull] ILexer<Terminal> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
