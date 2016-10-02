using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.ALPHA
{
    /// <summary>Creates instances of the <see cref="AlphaLexer" /> class.</summary>
    public class AlphaLexerFactory : ILexerFactory<Alpha>
    {
        private ILexer<Alpha> instance;

        static AlphaLexerFactory()
        {
            Default = new AlphaLexerFactory(
                ABNF.ValueRangeLexerFactory.Default,
                ABNF.AlternationLexerFactory.Default);
        }

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
            ValueRangeLexerFactory = valueRangeLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
        }

        [NotNull]
        public static AlphaLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public IValueRangeLexerFactory ValueRangeLexerFactory { get; }

        /// <inheritdoc />
        public ILexer<Alpha> Create()
        {
            var upperCaseValueRangeLexer = ValueRangeLexerFactory.Create('\x41', '\x5A');
            var lowerCaseValueRangeLexer = ValueRangeLexerFactory.Create('\x61', '\x7A');
            var innerLexer = AlternationLexerFactory.Create(
                upperCaseValueRangeLexer,
                lowerCaseValueRangeLexer);
            return new AlphaLexer(innerLexer);
        }

        [NotNull]
        public AlphaLexerFactory UseAlternationLexerFactory([NotNull] IAlternationLexerFactory alternationLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            return new AlphaLexerFactory(
                ValueRangeLexerFactory,
                alternationLexerFactory);
        }

        [NotNull]
        public AlphaLexerFactory UseValueRangeFactory([NotNull] IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            return new AlphaLexerFactory(
                valueRangeLexerFactory,
                AlternationLexerFactory);
        }
    }
}
