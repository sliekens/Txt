using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.HTAB
{
    public sealed class HorizontalTabLexer : RuleLexer<HorizontalTab>, IInitializable
    {
        public HorizontalTabLexer([NotNull] Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Terminal> InnerLexer { get; private set; }

        public void Initialize()
        {
            InnerLexer = Terminal.Create("\x09", StringComparer.Ordinal);
        }

        protected override IEnumerable<HorizontalTab> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new HorizontalTab(terminal);
            }
        }
    }
}
