using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Txt.ABNF.Core.HEXDIG
{
    public sealed class HexadecimalDigitLexer : RuleLexer<HexadecimalDigit>
    {
        public HexadecimalDigitLexer([NotNull] ILexer<Digit> digit)
        {
            if (digit == null)
            {
                throw new ArgumentNullException(nameof(digit));
            }
            InnerLexer = Alternation.Create(
                digit,
                Terminal.Create(@"A", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"B", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"C", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"D", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"E", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"F", StringComparer.OrdinalIgnoreCase));
        }

        [NotNull]
        public ILexer<Alternation> InnerLexer { get; }

        protected override IEnumerable<HexadecimalDigit> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var alternation in InnerLexer.Read(scanner, context))
            {
                yield return new HexadecimalDigit(alternation);
            }
        }
    }
}
