using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CHAR
{
    /// <summary>Creates instances of the <see cref="CharacterLexer" /> class.</summary>
    public class CharacterLexerFactory : LexerFactory<Character>
    {
        static CharacterLexerFactory()
        {
            Default = new CharacterLexerFactory(ABNF.ValueRangeLexerFactory.Default);
        }

        /// <summary>
        /// </summary>
        /// <param name="valueRangeLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CharacterLexerFactory([NotNull] IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            ValueRangeLexerFactory = valueRangeLexerFactory;
        }

        [NotNull]
        public static CharacterLexerFactory Default { get; }

        [NotNull]
        public IValueRangeLexerFactory ValueRangeLexerFactory { get; }

        /// <inheritdoc />
        public override ILexer<Character> Create()
        {
            var innerLexer = ValueRangeLexerFactory.Create('\x01', '\x7F');
            return new CharacterLexer(innerLexer);
        }

        [NotNull]
        public CharacterLexerFactory UseValueRangeLexerFactory([NotNull] IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            return new CharacterLexerFactory(valueRangeLexerFactory);
        }
    }
}
