namespace SLANG.Core.HEXDIG
{
    using System;

    using SLANG.Core.DIGIT;

    public class HexadecimalDigitLexerFactory : ILexerFactory<HexadecimalDigit>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly ITerminalsLexerFactory terminalsLexerFactory;

        public HexadecimalDigitLexerFactory(
            ILexerFactory<Digit> digitLexerFactory,
            ITerminalsLexerFactory terminalsLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException("digitLexerFactory", "Precondition: digitLexerFactory != null");
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

            this.digitLexerFactory = digitLexerFactory;
            this.terminalsLexerFactory = terminalsLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<HexadecimalDigit> Create()
        {
            var hexadecimalDigitAlternativeLexer = this.alternativeLexerFactory.Create(
                this.digitLexerFactory.Create(),
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