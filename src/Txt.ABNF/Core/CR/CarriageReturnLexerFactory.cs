using System;
using System.Diagnostics;
using Jetbrains.Annotations;
using Txt;

namespace Text.ABNF.Core.CR
{
    /// <summary>Creates instances of the <see cref="CarriageReturnLexer" /> class.</summary>
    public class CarriageReturnLexerFactory : ILexerFactory<CarriageReturn>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="terminalLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CarriageReturnLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
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
            var carriageReturnTerminalLexer = terminalLexerFactory.Create("\x0D", StringComparer.Ordinal);
            return new CarriageReturnLexer(carriageReturnTerminalLexer);
        }
    }
}
