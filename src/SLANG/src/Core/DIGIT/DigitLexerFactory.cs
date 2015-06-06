namespace SLANG.Core
{
    using System;

    public class DigitLexerFactory : ILexerFactory<Digit>
    {
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public DigitLexerFactory(IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "valueRangeLexerFactory",
                    "Precondition: valueRangeLexerFactory != null");
            }

            this.valueRangeLexerFactory = valueRangeLexerFactory;
        }

        public ILexer<Digit> Create()
        {
            var digitValueRangeLexer = this.valueRangeLexerFactory.Create('\x30', '\x39');
            return new DigitLexer(digitValueRangeLexer);
        }
    }
}