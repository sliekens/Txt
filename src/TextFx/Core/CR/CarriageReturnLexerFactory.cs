namespace TextFx.Core
{
    using System;

    /// <summary>Creates instances of the <see cref="CarriageReturnLexer" /> class.</summary>
    public class CarriageReturnLexerFactory : ILexerFactory<CarriageReturn>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public CarriageReturnLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<CarriageReturn> Create()
        {
            var carriageReturnTerminalLexer = this.terminalLexerFactory.Create('\x0D');
            return new CarriageReturnLexer(carriageReturnTerminalLexer);
        }
    }
}