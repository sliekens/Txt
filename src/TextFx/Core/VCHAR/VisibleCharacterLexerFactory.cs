namespace TextFx.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="VisibleCharacterLexer" /> class.</summary>
    public class VisibleCharacterLexerFactory : ILexerFactory<VisibleCharacter>
    {
        private readonly IValueRangeLexerFactory valueRangeLexer;

        public VisibleCharacterLexerFactory(IValueRangeLexerFactory valueRangeLexer)
        {
            if (valueRangeLexer == null)
            {
                throw new ArgumentNullException("valueRangeLexer");
            }

            this.valueRangeLexer = valueRangeLexer;
        }

        /// <inheritdoc />
        public ILexer<VisibleCharacter> Create()
        {
            return new VisibleCharacterLexer(this.valueRangeLexer.Create('\x21', '\x7E'));
        }
    }
}