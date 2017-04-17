using System;
using Txt.Core;

namespace Txt.ABNF.Core.HEXDIG
{
    public class HexadecimalDigitParser : Parser<HexadecimalDigit, int>
    {
        protected override int ParseImpl(HexadecimalDigit value)
        {
            return Convert.ToInt32(value.Text, 16);
        }
    }
}
