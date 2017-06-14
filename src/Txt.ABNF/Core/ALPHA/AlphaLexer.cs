using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.ALPHA
{
    public class AlphaLexer : RuleLexer<Alpha>, IInitializable
    {
        public AlphaLexer([NotNull] Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Alternation> InnerLexer { get; private set; }

        public void Initialize()
        {
            var upperCaseValueRangeLexer = ValueRange.Create('\x41', '\x5A');
            var lowerCaseValueRangeLexer = ValueRange.Create('\x61', '\x7A');
            InnerLexer = Alternation.Create(upperCaseValueRangeLexer, lowerCaseValueRangeLexer);
        }

        protected override IEnumerable<Alpha> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var element in InnerLexer.Read(scanner, context))
            {
                yield return new Alpha(element);
            }
        }
    }
}
