namespace SLANG.Core
{
    using System;

    public class CharacterLexerFactory : ILexerFactory<Character>
    {
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public CharacterLexerFactory(IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "valueRangeLexerFactory",
                    "Precondition: valueRangeLexerFactory != null");
            }

            this.valueRangeLexerFactory = valueRangeLexerFactory;
        }

        public ILexer<Character> Create()
        {
            var valueRangeLexer = this.valueRangeLexerFactory.Create('\x01', '\x7F');
            return new CharacterLexer(valueRangeLexer);
        }
    }
}