using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.BIT
{
    public sealed class BitLexer : RuleLexer<Bit>
    {
        public BitLexer()
        {
            InnerLexer = Alternation.Create(
                Terminal.Create(@"0", StringComparer.Ordinal),
                Terminal.Create(@"1", StringComparer.Ordinal));
        }

        [NotNull]
        public ILexer<Alternation> InnerLexer { get; }

        protected override IEnumerable<Bit> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var alternation in InnerLexer.Read(scanner, context))
            {
                yield return new Bit(alternation);
            }
        }
    }
}
