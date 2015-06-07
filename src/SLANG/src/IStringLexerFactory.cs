namespace SLANG
{
    /// <summary>Provides the interface for factory classes that create lexers for a string of terminal values.</summary>
    public interface IStringLexerFactory
    {
        /// <summary>
        ///     Initializes a new instance of a class that implements the <see cref="ILexer{TElement}" /> interface for a
        ///     given collection of terminal values.
        /// </summary>
        /// <param name="terminals">The terminal values.</param>
        /// <returns>>An instance of a class that implements <see cref="ILexer{TElement}" /> for the given terminal values.</returns>
        ILexer<TerminalString> Create(char[] terminals);

        /// <summary>
        ///     Initializes a new instance of a class that implements the <see cref="ILexer{TElement}" /> interface for a
        ///     given string of terminal values.
        /// </summary>
        /// <param name="terminals">The terminal values.</param>
        /// <returns>>An instance of a class that implements <see cref="ILexer{TElement}" /> for the given terminal values.</returns>
        ILexer<TerminalString> Create(string terminals);
    }
}