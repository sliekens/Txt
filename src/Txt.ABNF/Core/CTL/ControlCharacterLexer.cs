using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CTL
{
    public sealed class ControlCharacterLexer : RuleLexer<ControlCharacter>
    {
        public ControlCharacterLexer()
        {
            InnerLexer = Alternation.Create(
                ValueRange.Create('\x00', '\x1F'),
                Terminal.Create("\x7F", StringComparer.Ordinal));
        }

        [NotNull]
        public ILexer<Alternation> InnerLexer { get; }

        protected override IEnumerable<ControlCharacter> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var alternation in InnerLexer.Read(scanner, context))
            {
                yield return new ControlCharacter(alternation);
            }
        }
    }
}
