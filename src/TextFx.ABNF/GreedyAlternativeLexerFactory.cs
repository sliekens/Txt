namespace TextFx.ABNF
{
    using System;
    using JetBrains.Annotations;

    public class GreedyAlternativeLexerFactory : IAlternativeLexerFactory
    {
        public ILexer<Alternative> Create([ItemNotNull] params ILexer[] lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException(nameof(lexers));
            }
            return new GreedyAlternativeLexer(lexers);
        }
    }
}
