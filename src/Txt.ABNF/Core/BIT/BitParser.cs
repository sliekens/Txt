using System;
using Txt.Core;

namespace Txt.ABNF.Core.BIT
{
    public class BitParser : Parser<Bit, bool>
    {
        protected override bool ParseImpl(Bit value)
        {
            switch (value.Text)
            {
                case "0":
                    return false;
                case "1":
                    return true;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
