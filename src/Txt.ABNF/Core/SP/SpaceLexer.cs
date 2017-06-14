using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.SP
{
    public sealed class SpaceLexer : RuleLexer<Space>, IInitializable
    {
        public SpaceLexer([NotNull] Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            InnerLexer = Terminal.Create("\x20", StringComparer.Ordinal);
        }

        protected override IEnumerable<Space> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new Space(terminal);
            }
        }
    }
}
