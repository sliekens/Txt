using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.LF
{
    public sealed class LineFeedLexer : RuleLexer<LineFeed>, IInitializable
    {
        public LineFeedLexer([NotNull] Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            InnerLexer = Terminal.Create("\x0A", StringComparer.Ordinal);
        }

        protected override IEnumerable<LineFeed> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new LineFeed(terminal);
            }
        }
    }
}
