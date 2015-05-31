namespace SLANG.Core
{
    public class DigitValueRangeLexer : ValueRangeLexer
    {
        public DigitValueRangeLexer()
            : base('\x30', '\x39')
        {
        }
    }
}