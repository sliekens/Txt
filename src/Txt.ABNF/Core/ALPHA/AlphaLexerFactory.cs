using System;
using System.Diagnostics;
using Jetbrains.Annotations;

namespace Txt.ABNF.Core.ALPHA
{
    /// <summary>Creates instances of the <see cref="AlphaLexer" /> class.</summary>
    public class AlphaLexerFactory : ILexerFactory<Alpha>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueRangeLexerFactory"></param>
        /// <param name="alternativeLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AlphaLexerFactory(
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory,
            [NotNull] IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }
            this.valueRangeLexerFactory = valueRangeLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<Alpha> Create()
        {
            var upperCaseValueRangeLexer = valueRangeLexerFactory.Create('\x41', '\x5A');
            var lowerCaseValueRangeLexer = valueRangeLexerFactory.Create('\x61', '\x7A');
            var upperOrLowerCaseAlphaLexer = alternativeLexerFactory.Create(
                upperCaseValueRangeLexer,
                lowerCaseValueRangeLexer);
            return new AlphaLexer(upperOrLowerCaseAlphaLexer);
        }
    }
}
