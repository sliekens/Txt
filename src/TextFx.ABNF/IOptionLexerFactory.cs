namespace TextFx.ABNF
{
    /// <summary>Provides the interface for factory classes that create lexers for an optional element.</summary>
    public interface IOptionLexerFactory
    {
        /// <summary>
        ///     Initializes a new instance of a class that implements the <see cref="ILexer{TElement}" /> interface for an
        ///     optional element.
        /// </summary>
        /// <param name="lexer">The lexer for the optional element.</param>
        /// <returns>>An instance of a class that implements <see cref="ILexer{TElement}" /> for the given option.</returns>
        ILexer<Repetition> Create(ILexer lexer);
    }
}