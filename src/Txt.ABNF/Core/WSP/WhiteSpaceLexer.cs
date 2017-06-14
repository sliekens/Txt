using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.WSP
{
    public sealed class WhiteSpaceLexer : RuleLexer<WhiteSpace>, IInitializable
    {
        public WhiteSpaceLexer([NotNull] Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            InnerLexer = Alternation.Create(Grammar.Rule("SP"), Grammar.Rule("HTAB"));
        }

        protected override IEnumerable<WhiteSpace> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var alternation in InnerLexer.Read(scanner, context))
            {
                yield return new WhiteSpace(alternation);
            }
        }
    }
}
