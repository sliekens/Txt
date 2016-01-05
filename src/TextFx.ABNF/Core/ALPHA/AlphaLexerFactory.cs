namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;

    /// <summary>Creates instances of the <see cref="AlphaLexer" /> class.</summary>
    public class AlphaLexerFactory : ILexerFactory<Alpha>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public AlphaLexerFactory(
            IValueRangeLexerFactory valueRangeLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory)
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
            var upperCaseValueRangeLexer = this.valueRangeLexerFactory.Create('\x41', '\x5A');
            var lowerCaseValueRangeLexer = this.valueRangeLexerFactory.Create('\x61', '\x7A');
            var upperOrLowerCaseAlphaLexer = this.alternativeLexerFactory.Create(
                upperCaseValueRangeLexer,
                lowerCaseValueRangeLexer);
            return new AlphaLexer(upperOrLowerCaseAlphaLexer);
        }
    }
}