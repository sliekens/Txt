using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CRLF
{
    public sealed class NewLineLexer : RuleLexer<NewLine>, IInitializable
    {
        public NewLineLexer([NotNull] Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            var cr = Grammar.Rule("CR");
            var lf = Grammar.Rule("LF");
            InnerLexer = Concatenation.Create(cr, lf);
        }

        protected override IEnumerable<NewLine> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new NewLine(concatenation);
            }
        }
    }
}
