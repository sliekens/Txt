namespace SLANG
{
    /// <summary>Creates instances of the <see cref="AlternativeLexer" /> class.</summary>
    public class AlternativeLexerFactory : IAlternativeLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Alternative> Create(params ILexer[] lexers)
        {
            return new AlternativeLexer(lexers);
        }
    }
}