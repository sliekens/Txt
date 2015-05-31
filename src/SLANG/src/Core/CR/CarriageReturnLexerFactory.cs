namespace SLANG.Core.CR
{
    using System;

    public class CarriageReturnLexerFactory : ILexerFactory<CarriageReturn>
    {
        private readonly ITerminalsLexerFactory terminalsLexerFactory;

        public CarriageReturnLexerFactory(ITerminalsLexerFactory terminalsLexerFactory)
        {
            if (terminalsLexerFactory == null)
            {
                throw new ArgumentNullException("terminalsLexerFactory", "Precondition: terminalsLexerFactory != null");
            }

            this.terminalsLexerFactory = terminalsLexerFactory;
        }

        public ILexer<CarriageReturn> Create()
        {
            var carriageReturnTerminalLexer = this.terminalsLexerFactory.Create('\x0D');
            return new CarriageReturnLexer(carriageReturnTerminalLexer);
        }
    }
}
