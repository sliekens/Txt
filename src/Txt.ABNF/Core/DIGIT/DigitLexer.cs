using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DIGIT
{
    public sealed class DigitLexer : RuleLexer<Digit>
    {
        public DigitLexer()
        {
            InnerLexer = ValueRange.Create('\x30', '\x39');
        }

        [NotNull]
        public ILexer<Terminal> InnerLexer { get; }

        protected override IEnumerable<Digit> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new Digit(terminal);
            }
        }
    }
}
