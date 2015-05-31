namespace SLANG.Core.OCTET
{
    using System;

    public class OctetLexerFactory : ILexerFactory<Octet>
    {
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public OctetLexerFactory(IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException("valueRangeLexerFactory", "Precondition: valueRangeLexerFactory != null");
            }

            this.valueRangeLexerFactory = valueRangeLexerFactory;
        }

        public ILexer<Octet> Create()
        {
            var octetValueRangeLexer = this.valueRangeLexerFactory.Create('\x00', '\xFF');
            return new OctetLexer(octetValueRangeLexer);
        }
    }
}
