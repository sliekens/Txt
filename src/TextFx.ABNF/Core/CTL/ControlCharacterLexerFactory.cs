namespace TextFx.ABNF.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="ControlCharacterLexer" /> class.</summary>
    public class ControlCharacterLexerFactory : ILexerFactory<ControlCharacter>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public ControlCharacterLexerFactory(
            IValueRangeLexerFactory valueRangeLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException("valueRangeLexerFactory");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            this.valueRangeLexerFactory = valueRangeLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<ControlCharacter> Create()
        {
            var controlsValueRange = this.valueRangeLexerFactory.Create('\x00', '\x1F');
            var delete = this.terminalLexerFactory.Create("\x7F");
            var controlCharacterAlternativeLexer = this.alternativeLexerFactory.Create(controlsValueRange, delete);
            return new ControlCharacterLexer(controlCharacterAlternativeLexer);
        }
    }
}