using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.SP
{
    public sealed class SpaceLexer : RuleLexer<Space>
    {
        public SpaceLexer()
        {
            InnerLexer = Terminal.Create("\x20", StringComparer.Ordinal);
        }

        [NotNull]
        public ILexer<Terminal> InnerLexer { get; }

        protected override IEnumerable<Space> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new Space(terminal);
            }
        }
    }
}
