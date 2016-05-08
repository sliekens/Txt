using Txt.Core;
using Txt.ABNF;

namespace Sample1
{
    using System;
    
    

    public sealed class SignLexerFactory : ILexerFactory<Sign>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public SignLexerFactory(
            IAlternationLexerFactory alternationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<Sign> Create()
        {
            var plusLexer = terminalLexerFactory.Create("+", StringComparer.Ordinal);
            var minusLexer = terminalLexerFactory.Create("-", StringComparer.Ordinal);
            var plusOrMinusLexer = alternationLexerFactory.Create(plusLexer, minusLexer);
            return new SignLexer(plusOrMinusLexer);
        }
    }
}
