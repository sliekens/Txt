using System;
using System.Diagnostics;
using Jetbrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.BIT
{
    /// <summary>Creates instances of the <see cref="BitLexer" /> class.</summary>
    public class BitLexerFactory : ILexerFactory<Bit>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternationLexerFactory alternationLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        /// <summary>
        /// </summary>
        /// <param name="alternationLexerFactory"></param>
        /// <param name="terminalLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public BitLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ITerminalLexerFactory terminalLexerFactory)
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

        /// <inheritdoc />
        public ILexer<Bit> Create()
        {
            var bit0TerminalLexer = terminalLexerFactory.Create("0", StringComparer.Ordinal);
            var bit1TerminalLexer = terminalLexerFactory.Create("1", StringComparer.Ordinal);
            var innerLexer = alternationLexerFactory.Create(bit0TerminalLexer, bit1TerminalLexer);
            return new BitLexer(innerLexer);
        }
    }
}
