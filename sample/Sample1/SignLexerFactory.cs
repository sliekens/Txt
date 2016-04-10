using Txt;

namespace Sample1
{
    using System;
    using Text;
    using Text.ABNF;

    public sealed class SignLexerFactory : ILexerFactory<Sign>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public SignLexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            ITerminalLexerFactory terminalLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<Sign> Create()
        {
            var plusLexer = terminalLexerFactory.Create("+", StringComparer.Ordinal);
            var minusLexer = terminalLexerFactory.Create("-", StringComparer.Ordinal);
            var plusOrMinusLexer = alternativeLexerFactory.Create(plusLexer, minusLexer);
            return new SignLexer(plusOrMinusLexer);
        }
    }
}
