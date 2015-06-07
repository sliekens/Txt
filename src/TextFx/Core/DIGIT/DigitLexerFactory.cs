namespace TextFx.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="DigitLexer" /> class.</summary>
    public class DigitLexerFactory : ILexerFactory<Digit>
    {
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public DigitLexerFactory(IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException("valueRangeLexerFactory");
            }

            this.valueRangeLexerFactory = valueRangeLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<Digit> Create()
        {
            var digitValueRangeLexer = this.valueRangeLexerFactory.Create('\x30', '\x39');
            return new DigitLexer(digitValueRangeLexer);
        }
    }
}