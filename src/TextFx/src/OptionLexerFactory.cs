namespace TextFx
{
    /// <summary>Creates instances of the <see cref="OptionLexer" /> class.</summary>
    public class OptionLexerFactory : IOptionLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Repetition> Create(ILexer lexer)
        {
            return new OptionLexer(lexer);
        }
    }
}