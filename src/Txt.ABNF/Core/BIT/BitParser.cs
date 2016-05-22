using System;
using Txt.Core;

namespace Txt.ABNF.Core.BIT
{
    public class BitParser : Parser<Bit, bool>
    {
        protected override bool ParseImpl(Bit value)
        {
            if (value.Ordinal == 1)
            {
                return false;
            }
            if (value.Ordinal == 2)
            {
                return true;
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}
