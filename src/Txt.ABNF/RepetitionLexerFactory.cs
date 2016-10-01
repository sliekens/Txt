using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>Creates instances of the <see cref="RepetitionLexer" /> class.</summary>
    public class RepetitionLexerFactory : IRepetitionLexerFactory
    {
        [NotNull]
        public static RepetitionLexerFactory Default { get; } = new RepetitionLexerFactory();

        /// <inheritdoc />
        public ILexer<Repetition> Create(ILexer<Element> lexer, int lowerBound, int upperBound)
        {
            return new RepetitionLexer(lexer, lowerBound, upperBound);
        }
    }
}
