namespace SLANG.Core.ALPHA
{
    using System;

    public class AlphaLexerFactory : ILexerFactory<Alpha>
    {
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        public AlphaLexerFactory(IValueRangeLexerFactory valueRangeLexerFactory, IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException("valueRangeLexerFactory", "Precondition: valueRangeLexerFactory != null");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory", "Precondition: alternativeLexerFactory != null");
            }

            this.valueRangeLexerFactory = valueRangeLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<Alpha> Create()
        {
            var upperCaseValueRangeLexer = this.valueRangeLexerFactory.Create('\x41', '\x5A');
            var lowerCaseValueRangeLexer = this.valueRangeLexerFactory.Create('\x61', '\x7A');
            var upperOrLowerCaseAlphaLexer = this.alternativeLexerFactory.Create(upperCaseValueRangeLexer, lowerCaseValueRangeLexer);
            return new AlphaLexer(upperOrLowerCaseAlphaLexer);
        }
    }
}
