using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.HEXDIG
{
    public sealed class HexadecimalDigitLexer : RuleLexer<HexadecimalDigit>, IInitializable
    {
        public HexadecimalDigitLexer([NotNull] Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            InnerLexer = Alternation.Create(
                Grammar.Rule("DIGIT"),
                Terminal.Create(@"A", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"B", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"C", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"D", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"E", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"F", StringComparer.OrdinalIgnoreCase));
        }

        protected override IEnumerable<HexadecimalDigit> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var alternation in InnerLexer.Read(scanner, context))
            {
                yield return new HexadecimalDigit(alternation);
            }
        }
    }
}
