using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>Creates instances of the <see cref="RepetitionLexer" /> class.</summary>
    public class RepetitionLexerFactory : IRepetitionLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Repetition> Create(ILexer lexer, int lowerBound, int upperBound)
        {
            return new RepetitionLexer(lexer, lowerBound, upperBound);
        }
    }
}
