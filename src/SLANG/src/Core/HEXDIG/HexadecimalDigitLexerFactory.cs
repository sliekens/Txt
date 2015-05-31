namespace SLANG.Core.HEXDIG
{
    using System;

    public class HexadecimalDigitLexerFactory : ILexerFactory<HexadecimalDigit>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexer<Digit> digitLexer;

        private readonly ITerminalsLexerFactory terminalsLexerFactory;

        public HexadecimalDigitLexerFactory(
            ILexer<Digit> digitLexer,
            ITerminalsLexerFactory terminalsLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (digitLexer == null)
            {
                throw new ArgumentNullException("digitLexer", "Precondition: digitLexer != null");
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

            this.digitLexer = digitLexer;
            this.terminalsLexerFactory = terminalsLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<HexadecimalDigit> Create()
        {
            var hexadecimalDigitAlternativeLexer = this.alternativeLexerFactory.Create(
                this.digitLexer,
                this.terminalsLexerFactory.Create("A"),
                this.terminalsLexerFactory.Create("B"),
                this.terminalsLexerFactory.Create("C"),
                this.terminalsLexerFactory.Create("D"),
                this.terminalsLexerFactory.Create("E"),
                this.terminalsLexerFactory.Create("F"));
            return new HexadecimalDigitLexer(hexadecimalDigitAlternativeLexer);
        }
    }
}