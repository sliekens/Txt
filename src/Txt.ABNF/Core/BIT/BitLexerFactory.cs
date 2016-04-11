using System;
using System.Diagnostics;
using Jetbrains.Annotations;

namespace Txt.ABNF.Core.BIT
{
    /// <summary>Creates instances of the <see cref="BitLexer" /> class.</summary>
    public class BitLexerFactory : ILexerFactory<Bit>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alternativeLexerFactory"></param>
        /// <param name="terminalLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public BitLexerFactory(
            [NotNull] IAlternativeLexerFactory alternativeLexerFactory,
            [NotNull] ITerminalLexerFactory terminalLexerFactory)
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

        /// <inheritdoc />
        public ILexer<Bit> Create()
        {
            var bit0TerminalLexer = terminalLexerFactory.Create("0", StringComparer.Ordinal);
            var bit1TerminalLexer = terminalLexerFactory.Create("1", StringComparer.Ordinal);
            var bitAlternativeLexer = alternativeLexerFactory.Create(bit0TerminalLexer, bit1TerminalLexer);
            return new BitLexer(bitAlternativeLexer);
        }
    }
}
