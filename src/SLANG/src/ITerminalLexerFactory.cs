namespace SLANG
{
    /// <summary>Provides the interface for factory classes that create lexers for a terminal value.</summary>
    public interface ITerminalLexerFactory
    {
        /// <summary>
        ///     Initializes a new instance of a class that implements the <see cref="ILexer{TElement}" /> interface for a
        ///     given terminal value.
        /// </summary>
        /// <param name="terminal">The terminal value.</param>
        /// <returns>>An instance of a class that implements <see cref="ILexer{TElement}" /> for the given terminal value.</returns>
        ILexer<Terminal> Create(char terminal);
    }
}