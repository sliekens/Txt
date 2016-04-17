using System;
using Jetbrains.Annotations;

namespace Txt.ABNF
{
    public class GreedyAlternationLexerFactory : IAlternationLexerFactory
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
