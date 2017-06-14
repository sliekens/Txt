using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DQUOTE
{
    public sealed class DoubleQuoteLexer : RuleLexer<DoubleQuote>, IInitializable
    {
        public DoubleQuoteLexer([NotNull] Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            InnerLexer = Terminal.Create("\x22", StringComparer.Ordinal);
        }

        protected override IEnumerable<DoubleQuote> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new DoubleQuote(terminal);
            }
        }
    }
}
