using System;
using JetBrains.Annotations;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Txt.ABNF.Core.HEXDIG
{
    public class HexadecimalDigitParser : Parser<HexadecimalDigit, int>
    {
        private readonly IParser<Digit, int> digitParser;

        public HexadecimalDigitParser([NotNull] IParser<Digit, int> digitParser)
        {
            if (digitParser == null)
            {
                throw new ArgumentNullException(nameof(digitParser));
            }
            this.digitParser = digitParser;
        }

        protected override int ParseImpl(HexadecimalDigit value)
        {
            switch (value.Ordinal)
            {
                // 0-9
                case 1:
                    return digitParser.Parse((Digit)value.Element);

                // A
                case 2:
                    return 10;

                // B
                case 3:
                    return 11;

                // C
                case 4:
                    return 12;

                // D
                case 5:
                    return 13;

                // E
                case 6:
                    return 14;

                // F
                case 7:
                    return 15;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
