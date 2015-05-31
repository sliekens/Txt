namespace SLANG.Core.BIT
{
    using System;

    public class BitLexerFactory : ILexerFactory<Bit>
    {
        private readonly ITerminalsLexerFactory terminalsLexerFactory;

        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        public BitLexerFactory(IAlternativeLexerFactory alternativeLexerFactory, ITerminalsLexerFactory terminalsLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory", "Precondition: alternativeLexerFactory != null");
            }

            if (terminalsLexerFactory == null)
            {
                throw new ArgumentNullException("terminalsLexerFactory", "Precondition: terminalsLexerFactory != null");
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.terminalsLexerFactory = terminalsLexerFactory;
        }

        public ILexer<Bit> Create()
        {
            var bit0TerminalLexer = this.terminalsLexerFactory.Create("0");
            var bit1TerminalLexer = this.terminalsLexerFactory.Create("1");
            var bitAlternativeLexer = this.alternativeLexerFactory.Create(bit0TerminalLexer, bit1TerminalLexer);
            return new BitLexer(bitAlternativeLexer);
        }
    }
}
