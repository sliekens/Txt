using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Txt.ABNF.Core.HEXDIG
{
    /// <summary>Creates instances of the <see cref="HexadecimalDigitLexer" /> class.</summary>
    public class HexadecimalDigitLexerFactory : ILexerFactory<HexadecimalDigit>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternationLexerFactory alternationLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Digit> digitLexer;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        /// <summary>
        /// </summary>
        /// <param name="alternationLexerFactory"></param>
        /// <param name="terminalLexerFactory"></param>
        /// <param name="digitLexer"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public HexadecimalDigitLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] ILexer<Digit> digitLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (digitLexer == null)
            {
                throw new ArgumentNullException(nameof(digitLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.digitLexer = digitLexer;
        }

        /// <inheritdoc />
        public ILexer<HexadecimalDigit> Create()
        {
            var innerLexer = alternationLexerFactory.Create(
                digitLexer,
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
