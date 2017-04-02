using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.VCHAR
{
    public sealed class VisibleCharacterLexer : RuleLexer<VisibleCharacter>
    {
        public VisibleCharacterLexer()
        {
            InnerLexer = ValueRange.Create('\x21', '\x7E');
        }

        [NotNull]
        public ILexer<Terminal> InnerLexer { get; }

        protected override IEnumerable<VisibleCharacter> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new VisibleCharacter(terminal);
            }
        }
    }
}
