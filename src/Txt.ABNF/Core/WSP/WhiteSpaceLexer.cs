using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Txt.Core;

namespace Txt.ABNF.Core.WSP
{
    public sealed class WhiteSpaceLexer : RuleLexer<WhiteSpace>
    {
        public WhiteSpaceLexer([NotNull] ILexer<Space> sp, [NotNull] ILexer<HorizontalTab> htab)
        {
            if (sp == null)
            {
                throw new ArgumentNullException(nameof(sp));
            }
            if (htab == null)
            {
                throw new ArgumentNullException(nameof(htab));
            }
            InnerLexer = Alternation.Create(sp, htab);
        }

        [NotNull]
        public ILexer<Alternation> InnerLexer { get; }

        protected override IEnumerable<WhiteSpace> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var alternation in InnerLexer.Read(scanner, context))
            {
                yield return new WhiteSpace(alternation);
            }
        }
    }
}
