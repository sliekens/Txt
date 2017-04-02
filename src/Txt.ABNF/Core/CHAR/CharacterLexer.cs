using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CHAR
{
    public sealed class CharacterLexer : RuleLexer<Character>
    {
        public CharacterLexer()
        {
            InnerLexer = ValueRange.Create('\x01', '\x7F');
        }

        [NotNull]
        public ILexer<Terminal> InnerLexer { get; }

        protected override IEnumerable<Character> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new Character(terminal);
            }
        }
    }
}
