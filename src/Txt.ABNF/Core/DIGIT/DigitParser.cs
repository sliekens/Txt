using System;
using Txt.Core;

namespace Txt.ABNF.Core.DIGIT
{
    public class DigitParser : Parser<Digit, int>
    {
        protected override int ParseImpl(Digit value)
        {
            switch (value.Text)
            {
                case "0":
                    return 0;
                case "1":
                    return 1;
                case "2":
                    return 2;
                case "3":
                    return 3;
                case "4":
                    return 4;
                case "5":
                    return 5;
                case "6":
                    return 6;
                case "7":
                    return 7;
                case "8":
                    return 8;
                case "9":
                    return 9;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
