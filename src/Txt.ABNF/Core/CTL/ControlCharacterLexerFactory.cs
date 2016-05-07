using System;
using System.Diagnostics;
using Jetbrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CTL
{
    /// <summary>Creates instances of the <see cref="ControlCharacterLexer" /> class.</summary>
    public class ControlCharacterLexerFactory : ILexerFactory<ControlCharacter>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternationLexerFactory alternationLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITerminalLexerFactory terminalLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        /// <summary>
        /// </summary>
        /// <param name="valueRangeLexerFactory"></param>
        /// <param name="terminalLexerFactory"></param>
        /// <param name="alternationLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ControlCharacterLexerFactory(
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory,
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            this.valueRangeLexerFactory = valueRangeLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<ControlCharacter> Create()
        {
            var controlsValueRange = valueRangeLexerFactory.Create('\x00', '\x1F');
            var delete = terminalLexerFactory.Create("\x7F", StringComparer.Ordinal);
            var innerLexer = alternationLexerFactory.Create(controlsValueRange, delete);
            return new ControlCharacterLexer(innerLexer);
        }
    }
}
