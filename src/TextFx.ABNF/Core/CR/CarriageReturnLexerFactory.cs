namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;

    /// <summary>Creates instances of the <see cref="CarriageReturnLexer" /> class.</summary>
    public class CarriageReturnLexerFactory : ILexerFactory<CarriageReturn>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public CarriageReturnLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<CarriageReturn> Create()
        {
            var carriageReturnTerminalLexer = this.terminalLexerFactory.Create("\x0D", StringComparer.Ordinal);
            return new CarriageReturnLexer(carriageReturnTerminalLexer);
        }
    }
}