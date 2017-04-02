using System;
using JetBrains.Annotations;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Txt.ABNF.Core.HEXDIG
{
    public sealed class HexadecimalDigitLexerFactory : LexerFactory<HexadecimalDigit>
    {
        static HexadecimalDigitLexerFactory()
        {
            Default = new HexadecimalDigitLexerFactory(DIGIT.DigitLexerFactory.Default.Singleton());
        }

        public HexadecimalDigitLexerFactory([NotNull] ILexerFactory<Digit> digitLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            DigitLexerFactory = digitLexerFactory;
        }

        [NotNull]
        public static HexadecimalDigitLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Digit> DigitLexerFactory { get; }

        public override ILexer<HexadecimalDigit> Create()
        {
            return new HexadecimalDigitLexer(DigitLexerFactory.Create());
        }
    }
}
