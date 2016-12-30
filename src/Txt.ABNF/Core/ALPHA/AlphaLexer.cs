using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.ALPHA
{
    public class AlphaLexer : Lexer<Alpha>
    {
        public AlphaLexer([NotNull] ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
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
