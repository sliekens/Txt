using System;
using Jetbrains.Annotations;

namespace Txt.ABNF
{
    /// <summary>Creates instances of the <see cref="AlternationLexer" /> class.</summary>
    public class AlternationLexerFactory : IAlternationLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Alternation> Create([ItemNotNull] params ILexer[] lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException(nameof(lexers));
            }
            return new AlternationLexer(lexers);
        }
    }
}
