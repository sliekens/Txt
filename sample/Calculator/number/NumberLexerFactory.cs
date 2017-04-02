using System;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Calculator.number
{
    public sealed class NumberLexerFactory : LexerFactory<Number>
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
            return new NumberLexer(Digit.Create());
        }
    }
}
