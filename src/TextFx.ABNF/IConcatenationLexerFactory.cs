namespace TextFx.ABNF
{
    /// <summary>Provides the interface for factory classes that create lexers for a sequence of elements.</summary>
    public interface IConcatenationLexerFactory
    {
        /// <summary>
        ///     Initializes a new instance of a class that implements the <see cref="ILexer{TElement}" /> interface for a
        ///     given collection of sequential elements.
        /// </summary>
        /// <param name="lexers">A collection of lexers, one for each element in the sequence, in order of appearance.</param>
        /// <returns>An instance of a class that implements <see cref="ILexer{TElement}" /> for the given sequence.</returns>
        ILexer<Concatenation> Create(params ILexer[] lexers);
    }
}