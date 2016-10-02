using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.VCHAR
{
    /// <summary>Creates instances of the <see cref="VisibleCharacterLexer" /> class.</summary>
    public class VisibleCharacterLexerFactory : LexerFactory<VisibleCharacter>
    {
        private ILexer<VisibleCharacter> instance;

        static VisibleCharacterLexerFactory()
        {
            Default = new VisibleCharacterLexerFactory(ABNF.ValueRangeLexerFactory.Default);
        }

        /// <summary>
        /// </summary>
        /// <param name="valueRangeLexer"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public VisibleCharacterLexerFactory([NotNull] IValueRangeLexerFactory valueRangeLexer)
        {
            if (valueRangeLexer == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexer));
            }
            ValueRangeLexerFactory = valueRangeLexer;
        }

        [NotNull]
        public static VisibleCharacterLexerFactory Default { get; }

        [NotNull]
        public IValueRangeLexerFactory ValueRangeLexerFactory { get; }

        /// <inheritdoc />
        public override ILexer<VisibleCharacter> Create()
        {
            var innerLexer = ValueRangeLexerFactory.Create('\x21', '\x7E');
            return new VisibleCharacterLexer(innerLexer);
        }

        [NotNull]
        public VisibleCharacterLexerFactory UseValueRangeLexerFactory(
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            return new VisibleCharacterLexerFactory(valueRangeLexerFactory);
        }
    }
}
