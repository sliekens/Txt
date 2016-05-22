using System;
using Txt.Core;

namespace Txt.ABNF.Core.OCTET
{
    // I'm not sure about keeping this. It's not useful to read binary data as character data and then convert it back to binary. The OCTET rule is really only useful to describe grammars that have mixed character and binary data.
    public class OctetParser : Parser<Octet, byte[]>
    {
        protected override byte[] ParseImpl(Octet value)
        {
            // Generally, people shouldn't use this class. Especially not with multibyte character encodings. But you just know that someone will try. For that reason, return a byte array instead of a single byte.
            int byteCount;
            if (value.Value <= 255)
            {
                byteCount = 1;
            }
            else if (value.Value <= 65535)
            {
                byteCount = 2;
            }
            else if (value.Value <= 16777215)
            {
                byteCount = 3;
            }
            else
            {
                byteCount = 4;
            }
            var result = new byte[byteCount];
            Array.Copy(BitConverter.GetBytes(value.Value), 0, result, 0, byteCount);
            return result;
        }
    }
}
