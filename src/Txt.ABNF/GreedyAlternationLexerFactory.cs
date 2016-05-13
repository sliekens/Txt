using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    public class GreedyAlternationLexerFactory : IGreedyAlternationLexerFactory
    {
        public ILexer<Alternation> Create([ItemNotNull] params ILexer[] lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException(nameof(lexers));
            }
            return new GreedyAlternativeLexer(lexers);
        }
    }
}
