namespace TextFx.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="BitLexer" /> class.</summary>
    public class BitLexerFactory : ILexerFactory<Bit>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public BitLexerFactory(IAlternativeLexerFactory alternativeLexerFactory, IStringLexerFactory stringLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<Bit> Create()
        {
            var bit0TerminalLexer = this.stringLexerFactory.Create("0");
            var bit1TerminalLexer = this.stringLexerFactory.Create("1");
            var bitAlternativeLexer = this.alternativeLexerFactory.Create(bit0TerminalLexer, bit1TerminalLexer);
            return new BitLexer(bitAlternativeLexer);
        }
    }
}