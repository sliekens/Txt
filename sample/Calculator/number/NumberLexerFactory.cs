using System;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Calculator.number
{
    public sealed class NumberLexerFactory : RuleLexerFactory<Number>
    {
        static NumberLexerFactory()
        {
            Default = new NumberLexerFactory(DigitLexerFactory.Default.Singleton());
        }

        public NumberLexerFactory(ILexerFactory<Digit> digitLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            Digit = digitLexerFactory;
        }

        public static NumberLexerFactory Default { get; }

        public ILexerFactory<Digit> Digit { get; }

        public override ILexer<Number> Create()
        {
            var digit = Digit.Create();
            var digits = Repetition.Create(digit, 1, int.MaxValue);
            var fraction = Concatenation.Create(Terminal.Create(@".", StringComparer.Ordinal), digits);
            var innerLexer = Alternation.Create(fraction, Concatenation.Create(digits, Option.Create(fraction)));
            return new NumberLexer(innerLexer);
        }
    }
}
