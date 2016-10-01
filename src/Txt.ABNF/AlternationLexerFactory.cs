using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>Creates instances of the <see cref="AlternationLexer" /> class.</summary>
    public class AlternationLexerFactory : IAlternationLexerFactory
    {
        [NotNull]
        public static AlternationLexerFactory Default { get; } = new AlternationLexerFactory();

        /// <inheritdoc />
        public ILexer<Alternation> Create(params ILexer<Element>[] lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException(nameof(lexers));
            }
            return new AlternationLexer(lexers);
        }
    }
}
