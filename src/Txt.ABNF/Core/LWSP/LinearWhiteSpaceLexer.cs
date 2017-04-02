using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.WSP;
using Txt.Core;

namespace Txt.ABNF.Core.LWSP
{
    public sealed class LinearWhiteSpaceLexer : RuleLexer<LinearWhiteSpace>
    {
        public LinearWhiteSpaceLexer([NotNull] ILexer<WhiteSpace> wsp, [NotNull] ILexer<NewLine> crlf)
        {
            if (wsp == null)
            {
                throw new ArgumentNullException(nameof(wsp));
            }
            if (crlf == null)
            {
                throw new ArgumentNullException(nameof(crlf));
            }
            InnerLexer = Repetition.Create(Alternation.Create(wsp, Concatenation.Create(crlf, wsp)), 0, int.MaxValue);
        }

        [NotNull]
        public ILexer<Repetition> InnerLexer { get; }

        protected override IEnumerable<LinearWhiteSpace> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var repetition in InnerLexer.Read(scanner, context))
            {
                yield return new LinearWhiteSpace(repetition);
            }
        }
    }
}
