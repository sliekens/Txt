namespace TextFx
{
    /// <summary>Provides the interface for factory classes that create lexers for a set of alternative elements.</summary>
    public interface IAlternativeLexerFactory
    {
        /// <summary>
        ///     Initializes a new instance of a class that implements the <see cref="ILexer{TElement}" /> interface for a
        ///     given collection of alternative elements. Alternatives are evaluated with a first-match-wins algorithm, so order is
        ///     important.
        /// </summary>
        /// <param name="lexers">A collection of lexers, one for each alternative element.</param>
        /// <returns>An instance of a class that implements <see cref="ILexer{TElement}" /> for the given alternatives.</returns>
        ILexer<Alternative> Create(params ILexer[] lexers);
    }
}