using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CHAR
{
    public class CharacterLexer : CompositeLexer<Terminal, Character>
    {
        public CharacterLexer([NotNull] ILexer<Terminal> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
