using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DQUOTE
{
    public sealed class DoubleQuoteLexer : RuleLexer<DoubleQuote>
    {
        public DoubleQuoteLexer()
        {
            InnerLexer = Terminal.Create("\x22", StringComparer.Ordinal);
        }

        [NotNull]
        public ILexer<Terminal> InnerLexer { get; }

        protected override IEnumerable<DoubleQuote> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new DoubleQuote(terminal);
            }
        }
    }
}
