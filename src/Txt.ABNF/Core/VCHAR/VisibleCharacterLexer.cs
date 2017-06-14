using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.VCHAR
{
    public sealed class VisibleCharacterLexer : RuleLexer<VisibleCharacter>, IInitializable
    {
        public VisibleCharacterLexer([NotNull] Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            InnerLexer = ValueRange.Create('\x21', '\x7E');
        }

        protected override IEnumerable<VisibleCharacter> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new VisibleCharacter(terminal);
            }
        }
    }
}
