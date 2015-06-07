namespace SLANG.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="OctetLexer" /> class.</summary>
    public class OctetLexerFactory : ILexerFactory<Octet>
    {
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public OctetLexerFactory(IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException("valueRangeLexerFactory");
            }

            this.valueRangeLexerFactory = valueRangeLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<Octet> Create()
        {
            var innerLexer = this.valueRangeLexerFactory.Create('\x00', '\xFF');
            return new OctetLexer(innerLexer);
        }
    }
}