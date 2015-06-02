namespace SLANG.Core.BIT
{
    using System;

    public class BitLexerFactory : ILexerFactory<Bit>
    {
        private readonly IStringLexerFactory stringLexerFactory;

        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        public BitLexerFactory(IAlternativeLexerFactory alternativeLexerFactory, IStringLexerFactory stringLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory", "Precondition: alternativeLexerFactory != null");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory", "Precondition: stringLexerFactory != null");
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
        }

        public ILexer<Bit> Create()
        {
            var bit0TerminalLexer = this.stringLexerFactory.Create("0");
            var bit1TerminalLexer = this.stringLexerFactory.Create("1");
            var bitAlternativeLexer = this.alternativeLexerFactory.Create(bit0TerminalLexer, bit1TerminalLexer);
            return new BitLexer(bitAlternativeLexer);
        }
    }
}
