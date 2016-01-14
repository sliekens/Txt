namespace TextFx.ABNF
{
    using System;
    using JetBrains.Annotations;

    /// <summary>Creates instances of the <see cref="ConcatenationLexer" /> class.</summary>
    public class ConcatenationLexerFactory : IConcatenationLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Concatenation> Create([ItemNotNull] params ILexer[] lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException(nameof(lexers));
            }
            return new ConcatenationLexer(lexers);
        }
    }
}
