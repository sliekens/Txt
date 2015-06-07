namespace TextFx.ABNF.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="CharacterLexer" /> class.</summary>
    public class CharacterLexerFactory : ILexerFactory<Character>
    {
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public CharacterLexerFactory(IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException("valueRangeLexerFactory");
            }

            this.valueRangeLexerFactory = valueRangeLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<Character> Create()
        {
            var valueRangeLexer = this.valueRangeLexerFactory.Create('\x01', '\x7F');
            return new CharacterLexer(valueRangeLexer);
        }
    }
}