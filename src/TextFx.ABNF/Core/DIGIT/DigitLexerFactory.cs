namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;
    using JetBrains.Annotations;

    /// <summary>Creates instances of the <see cref="DigitLexer" /> class.</summary>
    public class DigitLexerFactory : ILexerFactory<Digit>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueRangeLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DigitLexerFactory([NotNull] IValueRangeLexerFactory valueRangeLexerFactory)
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
            var digitValueRangeLexer = valueRangeLexerFactory.Create('\x30', '\x39');
            return new DigitLexer(digitValueRangeLexer);
        }
    }
}
