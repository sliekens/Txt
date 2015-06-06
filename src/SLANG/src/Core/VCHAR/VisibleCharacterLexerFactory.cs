namespace SLANG.Core
{
    using System;

    public class VisibleCharacterLexerFactory : ILexerFactory<VisibleCharacter>
    {
        private readonly IValueRangeLexerFactory valueRangeLexer;

        public VisibleCharacterLexerFactory(IValueRangeLexerFactory valueRangeLexer)
        {
            if (valueRangeLexer == null)
            {
                throw new ArgumentNullException("valueRangeLexer", "Precondition: valueRangeLexer != null");
            }

            this.valueRangeLexer = valueRangeLexer;
        }

        public ILexer<VisibleCharacter> Create()
        {
            return new VisibleCharacterLexer(this.valueRangeLexer.Create('\x21', '\x7E'));
        }
    }
}