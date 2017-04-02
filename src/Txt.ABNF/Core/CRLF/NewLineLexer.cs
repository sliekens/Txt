using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF.Core.CR;
using Txt.ABNF.Core.LF;
using Txt.Core;

namespace Txt.ABNF.Core.CRLF
{
    public sealed class NewLineLexer : RuleLexer<NewLine>
    {
        public NewLineLexer([NotNull] ILexer<CarriageReturn> cr, [NotNull] ILexer<LineFeed> lf)
        {
            if (cr == null)
            {
                throw new ArgumentNullException(nameof(cr));
            }
            if (lf == null)
            {
                throw new ArgumentNullException(nameof(lf));
            }
            InnerLexer = Concatenation.Create(cr, lf);
        }

        [NotNull]
        public ILexer<Concatenation> InnerLexer { get; }

        protected override IEnumerable<NewLine> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new NewLine(concatenation);
            }
        }
    }
}
