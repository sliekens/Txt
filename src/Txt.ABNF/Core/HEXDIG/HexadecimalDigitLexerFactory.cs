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
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexerFactory<Digit> digitLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="digitLexerFactory"></param>
        /// <param name="terminalLexerFactory"></param>
        /// <param name="alternativeLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public HexadecimalDigitLexerFactory(
            [NotNull] ILexerFactory<Digit> digitLexerFactory,
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }
            this.digitLexerFactory = digitLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<HexadecimalDigit> Create()
        {
            var hexadecimalDigitAlternativeLexer = alternativeLexerFactory.Create(
                digitLexerFactory.Create(),
                terminalLexerFactory.Create("A", StringComparer.OrdinalIgnoreCase),
                terminalLexerFactory.Create("B", StringComparer.OrdinalIgnoreCase),
                terminalLexerFactory.Create("C", StringComparer.OrdinalIgnoreCase),
                terminalLexerFactory.Create("D", StringComparer.OrdinalIgnoreCase),
                terminalLexerFactory.Create("E", StringComparer.OrdinalIgnoreCase),
                terminalLexerFactory.Create("F", StringComparer.OrdinalIgnoreCase));
            return new HexadecimalDigitLexer(hexadecimalDigitAlternativeLexer);
        }
    }
}
