using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CR
{
    public sealed class CarriageReturnLexer : RuleLexer<CarriageReturn>, IInitializable
    {
        public CarriageReturnLexer([NotNull] Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            InnerLexer = Terminal.Create("\x0D", StringComparer.Ordinal);
        }

        protected override IEnumerable<CarriageReturn> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new CarriageReturn(terminal);
            }
        }
    }
}
