namespace SLANG.Core.CTL
{
    using System;

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
                throw new ArgumentNullException(
                    "valueRangeLexerFactory",
                    "Precondition: valueRangeLexerFactory != null");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory", "Precondition: terminalLexerFactory != null");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "alternativeLexerFactory",
                    "Precondition: alternativeLexerFactory != null");
            }

            this.valueRangeLexerFactory = valueRangeLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<ControlCharacter> Create()
        {
            var controlsValueRange = this.valueRangeLexerFactory.Create('\x00', '\x1F');
            var delete = this.terminalLexerFactory.Create('\x7F');
            var controlCharacterAlternativeLexer = this.alternativeLexerFactory.Create(controlsValueRange, delete);
            return new ControlCharacterLexer(controlCharacterAlternativeLexer);
        }
    }
}