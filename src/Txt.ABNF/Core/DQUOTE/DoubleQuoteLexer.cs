using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.DQUOTE
{
    public sealed class DoubleQuoteLexer : Lexer<DoubleQuote>
    {
        public DoubleQuoteLexer([NotNull] ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Terminal> InnerLexer { get; }

        protected override IEnumerable<DoubleQuote> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new DoubleQuote(terminal);
            }
        }
    }
}
