namespace SLANG
{
    using System;
    using System.Linq;

    public class StringLexerFactory : IStringLexerFactory
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public StringLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory", "Precondition: terminalLexerFactory != null");
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<TerminalString> Create(char[] terminals)
        {
            if (terminals == null)
            {
                throw new ArgumentNullException("terminals", "Precondition: terminals != null");
            }

            var lexers = terminals.Select(this.terminalLexerFactory.Create).ToList();
            return new StringLexer(lexers);
        }

        public ILexer<TerminalString> Create(string terminals)
        {
            if (terminals == null)
            {
                throw new ArgumentNullException("terminals", "Precondition: terminals != null");
            }

            return this.Create(terminals.ToCharArray());
        }
    }
}