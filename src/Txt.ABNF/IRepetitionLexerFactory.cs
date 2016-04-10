using Jetbrains.Annotations;
using Txt;

namespace Text.ABNF
{
    /// <summary>Provides the interface for factory classes that create lexers for a repeating element.</summary>
    public interface IRepetitionLexerFactory
    {
        /// <summary>
        ///     Initializes a new instance of a class that implements the <see cref="ILexer{TElement}" /> interface for a repeating
        ///     element with a specified lower and upper bound, both inclusive.
        /// </summary>
        /// <param name="lexer">The lexer for the repeating element.</param>
        /// <param name="lowerBound">A number that indicates the minimum number of occurrences (inclusive).</param>
        /// <param name="upperBound">A number that indicates the maximum number of occurrences (inclusive).</param>
        /// <returns>>An instance of a class that implements <see cref="ILexer{TElement}" /> for the given repetition.</returns>
        ILexer<Repetition> Create([NotNull] ILexer lexer, int lowerBound, int upperBound);
    }
}
