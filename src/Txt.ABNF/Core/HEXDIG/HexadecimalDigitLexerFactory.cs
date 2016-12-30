using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Txt.ABNF.Core.HEXDIG
{
    public sealed class HexadecimalDigitLexerFactory : RuleLexerFactory<HexadecimalDigit>
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
            var innerLexer = Alternation.Create(
                DigitLexerFactory.Create(),
                Terminal.Create(@"A", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"B", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"C", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"D", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"E", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"F", StringComparer.OrdinalIgnoreCase));
            return new HexadecimalDigitLexer(innerLexer);
        }
    }
}
