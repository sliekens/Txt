namespace TextFx
{
    using System;
    using System.Linq;

    /// <summary>Creates instances of the <see cref="StringLexer" /> class.</summary>
    public class StringLexerFactory : IStringLexerFactory
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StringLexerFactory" /> class with a given implementation of the
        ///     <see cref="ITerminalLexerFactory" /> interface. The given implementation determines whether strings are case
        ///     sensitive.
        /// </summary>
        /// <param name="terminalLexerFactory">An object that implements <see cref="ITerminalLexerFactory" />.</param>
        public StringLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<TerminalString> Create(char[] terminals)
        {
            if (terminals == null)
            {
                throw new ArgumentNullException("terminals");
            }

            var lexers = terminals.Select(this.terminalLexerFactory.Create).ToList();
            return new StringLexer(lexers);
        }

        /// <inheritdoc />
        public ILexer<TerminalString> Create(string terminals)
        {
            if (terminals == null)
            {
                throw new ArgumentNullException("terminals");
            }

            return this.Create(terminals.ToCharArray());
        }
    }
}