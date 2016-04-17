using System;
using System.Diagnostics;
using Jetbrains.Annotations;
using Txt.ABNF.Core.DIGIT;

namespace Txt.ABNF.Core.HEXDIG
{
    /// <summary>Creates instances of the <see cref="HexadecimalDigitLexer" /> class.</summary>
    public class HexadecimalDigitLexerFactory : ILexerFactory<HexadecimalDigit>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternationLexerFactory alternationLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<Digit> digitLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="digitLexerFactory"></param>
        /// <param name="terminalLexerFactory"></param>
        /// <param name="alternationLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public HexadecimalDigitLexerFactory(
            [NotNull] ILexerFactory<Digit> digitLexerFactory,
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            this.digitLexerFactory = digitLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<HexadecimalDigit> Create()
        {
            var innerLexer = alternationLexerFactory.Create(
                digitLexerFactory.Create(),
                terminalLexerFactory.Create("A", StringComparer.OrdinalIgnoreCase),
                terminalLexerFactory.Create("B", StringComparer.OrdinalIgnoreCase),
                terminalLexerFactory.Create("C", StringComparer.OrdinalIgnoreCase),
                terminalLexerFactory.Create("D", StringComparer.OrdinalIgnoreCase),
                terminalLexerFactory.Create("E", StringComparer.OrdinalIgnoreCase),
                terminalLexerFactory.Create("F", StringComparer.OrdinalIgnoreCase));
            return new HexadecimalDigitLexer(innerLexer);
        }
    }
}
