namespace TextFx.ABNF
{
    /// <summary>Creates instances of the <see cref="ConcatenationLexer" /> class.</summary>
    public class ConcatenationLexerFactory : IConcatenationLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Concatenation> Create(params ILexer[] lexers)
        {
            return new ConcatenationLexer(lexers);
        }
    }
}