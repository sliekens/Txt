using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CR
{
    public sealed class CarriageReturnLexer : RuleLexer<CarriageReturn>
    {
        public CarriageReturnLexer()
        {
            InnerLexer = Terminal.Create("\x0D", StringComparer.Ordinal);
        }

        [NotNull]
        public ILexer<Terminal> InnerLexer { get; }

        protected override IEnumerable<CarriageReturn> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new CarriageReturn(terminal);
            }
        }
    }
}
