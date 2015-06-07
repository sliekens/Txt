namespace TextFx.ABNF.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="HexadecimalDigitLexer" /> class.</summary>
    public class HexadecimalDigitLexerFactory : ILexerFactory<HexadecimalDigit>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public HexadecimalDigitLexerFactory(
            ILexerFactory<Digit> digitLexerFactory,
            IStringLexerFactory stringLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException("digitLexerFactory");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            this.digitLexerFactory = digitLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<HexadecimalDigit> Create()
        {
            var hexadecimalDigitAlternativeLexer = this.alternativeLexerFactory.Create(
                this.digitLexerFactory.Create(),
                this.stringLexerFactory.Create("A"),
                this.stringLexerFactory.Create("B"),
                this.stringLexerFactory.Create("C"),
                this.stringLexerFactory.Create("D"),
                this.stringLexerFactory.Create("E"),
                this.stringLexerFactory.Create("F"));
            return new HexadecimalDigitLexer(hexadecimalDigitAlternativeLexer);
        }
    }
}