using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DIGIT
{
    /// <summary>Creates instances of the <see cref="DigitLexer" /> class.</summary>
    public class DigitLexerFactory : LexerFactory<Digit>
    {
        private ILexer<Digit> instance;

        static DigitLexerFactory()
        {
            Default = new DigitLexerFactory(ABNF.ValueRangeLexerFactory.Default);
        }

        /// <summary>
        /// </summary>
        /// <param name="valueRangeLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DigitLexerFactory([NotNull] IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            ValueRangeLexerFactory = valueRangeLexerFactory;
        }

        [NotNull]
        public static DigitLexerFactory Default { get; }

        [NotNull]
        public IValueRangeLexerFactory ValueRangeLexerFactory { get; }

        /// <inheritdoc />
        public override ILexer<Digit> Create()
        {
            var innerLexer = ValueRangeLexerFactory.Create('\x30', '\x39');
            return new DigitLexer(innerLexer);
        }

        [NotNull]
        public DigitLexerFactory UseValueRangeLexerFactory([NotNull] IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            return new DigitLexerFactory(valueRangeLexerFactory);
        }
    }
}
