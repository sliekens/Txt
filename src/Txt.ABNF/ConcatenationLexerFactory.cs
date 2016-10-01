using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>Creates instances of the <see cref="ConcatenationLexer" /> class.</summary>
    public class ConcatenationLexerFactory : IConcatenationLexerFactory
    {
        [NotNull]
        public static ConcatenationLexerFactory Default { get; } = new ConcatenationLexerFactory();

        /// <inheritdoc />
        public ILexer<Concatenation> Create(params ILexer<Element>[] lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException(nameof(lexers));
            }
            return new ConcatenationLexer(lexers);
        }
    }
}
