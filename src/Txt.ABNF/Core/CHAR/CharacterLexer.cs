using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CHAR
{
    public sealed class CharacterLexer : RuleLexer<Character>, IInitializable
    {
        public CharacterLexer([NotNull] Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            InnerLexer = ValueRange.Create('\x01', '\x7F');
        }

        protected override IEnumerable<Character> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new Character(terminal);
            }
        }
    }
}
