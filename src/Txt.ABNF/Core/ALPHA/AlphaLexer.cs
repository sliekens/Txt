using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.ALPHA
{
    public class AlphaLexer : RuleLexer<Alpha>
    {
        public AlphaLexer()
        {
            var upperCaseValueRangeLexer = ValueRange.Create('\x41', '\x5A');
            var lowerCaseValueRangeLexer = ValueRange.Create('\x61', '\x7A');
            InnerLexer = Alternation.Create(upperCaseValueRangeLexer, lowerCaseValueRangeLexer);
        }

        [NotNull]
        public ILexer<Alternation> InnerLexer { get; }

        protected override IEnumerable<Alpha> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var element in InnerLexer.Read(scanner, context))
            {
                yield return new Alpha(element);
            }
        }
    }
}
