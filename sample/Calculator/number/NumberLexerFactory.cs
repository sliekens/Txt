using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Calculator.number
{
    public class NumberLexerFactory : ILexerFactory<Number>
    {
        private readonly ILexer<Digit> digitLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public NumberLexerFactory(IRepetitionLexerFactory repetitionLexerFactory, ILexer<Digit> digitLexer)
        {
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.digitLexer = digitLexer;
        }

        public ILexer<Number> Create()
        {
            return new NumberLexer(repetitionLexerFactory.Create(digitLexer, 1, int.MaxValue));
        }
    }
}
