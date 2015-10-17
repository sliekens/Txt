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
                this.terminalLexerFactory.Create("A", StringComparer.OrdinalIgnoreCase),
                this.terminalLexerFactory.Create("B", StringComparer.OrdinalIgnoreCase),
                this.terminalLexerFactory.Create("C", StringComparer.OrdinalIgnoreCase),
                this.terminalLexerFactory.Create("D", StringComparer.OrdinalIgnoreCase),
                this.terminalLexerFactory.Create("E", StringComparer.OrdinalIgnoreCase),
                this.terminalLexerFactory.Create("F", StringComparer.OrdinalIgnoreCase));
            return new HexadecimalDigitLexer(hexadecimalDigitAlternativeLexer);
        }
    }
}