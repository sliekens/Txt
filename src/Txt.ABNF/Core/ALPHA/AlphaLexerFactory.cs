using System;
using System.Diagnostics;
using Jetbrains.Annotations;

namespace Txt.ABNF.Core.ALPHA
{
    /// <summary>Creates instances of the <see cref="AlphaLexer" /> class.</summary>
    public class AlphaLexerFactory : ILexerFactory<Alpha>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternationLexerFactory alternationLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        /// <summary>
        /// </summary>
        /// <param name="valueRangeLexerFactory"></param>
        /// <param name="alternationLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AlphaLexerFactory(
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            this.valueRangeLexerFactory = valueRangeLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<Alpha> Create()
        {
            var upperCaseValueRangeLexer = valueRangeLexerFactory.Create('\x41', '\x5A');
            var lowerCaseValueRangeLexer = valueRangeLexerFactory.Create('\x61', '\x7A');
            var innerLexer = alternationLexerFactory.Create(
                upperCaseValueRangeLexer,
                lowerCaseValueRangeLexer);
            return new AlphaLexer(innerLexer);
        }
    }
}
