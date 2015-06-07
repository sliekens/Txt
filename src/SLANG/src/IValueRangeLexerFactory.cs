namespace SLANG
{
    using System;

    /// <summary>Provides the interface for factory classes that create lexers for a range of alternative values.</summary>
    public interface IValueRangeLexerFactory
    {
        /// <summary>
        ///     Initializes a new instance of a class that implements the <see cref="ILexer{TElement}" /> interface with a
        ///     specified lower and upper bound, both inclusive.
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range of alternatives (inclusive).</param>
        /// <param name="upperBound">The upper bound of the range of alternatives (inclusive).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The value of <paramref name="upperBound" /> is smaller than the value of
        ///     <paramref name="lowerBound" />.
        /// </exception>
        ILexer<Terminal> Create(char lowerBound, char upperBound);
    }
}