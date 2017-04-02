using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.LF
{
    public sealed class LineFeedLexer : RuleLexer<LineFeed>
    {
        public LineFeedLexer()
        {
            InnerLexer = Terminal.Create("\x0A", StringComparer.Ordinal);
        }

        [NotNull]
        public ILexer<Terminal> InnerLexer { get; }

        protected override IEnumerable<LineFeed> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new LineFeed(terminal);
            }
        }
    }
}
