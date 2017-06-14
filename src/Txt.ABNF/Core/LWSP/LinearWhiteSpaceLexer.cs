using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.LWSP
{
    public sealed class LinearWhiteSpaceLexer : RuleLexer<LinearWhiteSpace>, IInitializable
    {
        public LinearWhiteSpaceLexer([NotNull] Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            var wsp = Grammar.Rule("WSP");
            var crlf = Grammar.Rule("CRLF");
            InnerLexer = Repetition.Create(Alternation.Create(wsp, Concatenation.Create(crlf, wsp)), 0, int.MaxValue);
        }

        protected override IEnumerable<LinearWhiteSpace> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var repetition in InnerLexer.Read(scanner, context))
            {
                yield return new LinearWhiteSpace(repetition);
            }
        }
    }
}
