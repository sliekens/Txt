using System;
using JetBrains.Annotations;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Txt.ABNF.Core.HEXDIG
{
    public class HexadecimalDigitParserFactory : ParserFactory<HexadecimalDigit, int>
    {
        static HexadecimalDigitParserFactory()
        {
            Default = new HexadecimalDigitParserFactory(DIGIT.DigitParserFactory.Default.Singleton());
        }

        public HexadecimalDigitParserFactory([NotNull] IParserFactory<Digit, int> digitParserFactory)
        {
            if (digitParserFactory == null)
            {
                throw new ArgumentNullException(nameof(digitParserFactory));
            }
            DigitParserFactory = digitParserFactory;
        }

        [NotNull]
        public static HexadecimalDigitParserFactory Default { get; }

        [NotNull]
        public IParserFactory<Digit, int> DigitParserFactory { get; }

        public override IParser<HexadecimalDigit, int> Create()
        {
            return new HexadecimalDigitParser(DigitParserFactory.Create());
        }

        public HexadecimalDigitParserFactory UseDigitParserFactory(
            [NotNull] IParserFactory<Digit, int> digitParserFactory)
        {
            if (digitParserFactory == null)
            {
                throw new ArgumentNullException(nameof(digitParserFactory));
            }
            return new HexadecimalDigitParserFactory(digitParserFactory);
        }
    }
}
