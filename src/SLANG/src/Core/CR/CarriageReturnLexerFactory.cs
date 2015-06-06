namespace SLANG.Core.CR
{
    using System;

    public class CarriageReturnLexerFactory : ILexerFactory<CarriageReturn>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public CarriageReturnLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory", "Precondition: terminalLexerFactory != null");
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<CarriageReturn> Create()
        {
            var carriageReturnTerminalLexer = this.terminalLexerFactory.Create('\x0D');
            return new CarriageReturnLexer(carriageReturnTerminalLexer);
        }
    }
}