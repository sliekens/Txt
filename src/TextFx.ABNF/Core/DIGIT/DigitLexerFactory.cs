namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;

    /// <summary>Creates instances of the <see cref="DigitLexer" /> class.</summary>
    public class DigitLexerFactory : ILexerFactory<Digit>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public DigitLexerFactory(IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }

            this.valueRangeLexerFactory = valueRangeLexerFactory;
        }

        /// <inheritdoc />
        public ILexer<Digit> Create()
        {
            var digitValueRangeLexer = this.valueRangeLexerFactory.Create('\x30', '\x39');
            return new DigitLexer(digitValueRangeLexer);
        }
    }
}