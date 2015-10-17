namespace TextFx.ABNF.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="HexadecimalDigitLexer" /> class.</summary>
    public class HexadecimalDigitLexerFactory : ILexerFactory<HexadecimalDigit>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HexadecimalDigitLexerFactory(
            ILexerFactory<Digit> digitLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            this.digitLexerFactory = digitLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<HexadecimalDigit> Create()
        {
            var hexadecimalDigitAlternativeLexer = this.alternativeLexerFactory.Create(
                this.digitLexerFactory.Create(),
                this.terminalLexerFactory.Create("A"),
                this.terminalLexerFactory.Create("B"),
                this.terminalLexerFactory.Create("C"),
                this.terminalLexerFactory.Create("D"),
                this.terminalLexerFactory.Create("E"),
                this.terminalLexerFactory.Create("F"));
            return new HexadecimalDigitLexer(hexadecimalDigitAlternativeLexer);
        }
    }
}