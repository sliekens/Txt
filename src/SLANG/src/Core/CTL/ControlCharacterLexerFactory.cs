namespace SLANG.Core.CTL
{
    using System;

    public class ControlCharacterLexerFactory : ILexerFactory<ControlCharacter>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ITerminalsLexerFactory terminalsLexerFactory;

        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public ControlCharacterLexerFactory(
            IValueRangeLexerFactory valueRangeLexerFactory,
            ITerminalsLexerFactory terminalsLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "valueRangeLexerFactory",
                    "Precondition: valueRangeLexerFactory != null");
            }

            if (terminalsLexerFactory == null)
            {
                throw new ArgumentNullException("terminalsLexerFactory", "Precondition: terminalsLexerFactory != null");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "alternativeLexerFactory",
                    "Precondition: alternativeLexerFactory != null");
            }

            this.valueRangeLexerFactory = valueRangeLexerFactory;
            this.terminalsLexerFactory = terminalsLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<ControlCharacter> Create()
        {
            var controlsValueRange = this.valueRangeLexerFactory.Create('\x00', '\x1F');
            var delete = this.terminalsLexerFactory.Create('\x7F');
            var controlCharacterAlternativeLexer = this.alternativeLexerFactory.Create(controlsValueRange, delete);
            return new ControlCharacterLexer(controlCharacterAlternativeLexer);
        }
    }
}