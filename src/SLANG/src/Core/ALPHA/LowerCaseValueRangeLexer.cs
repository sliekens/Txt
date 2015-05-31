namespace SLANG.Core
{
    public class LowerCaseValueRangeLexer : ValueRangeLexer
    {
        public LowerCaseValueRangeLexer()
            : base('\x61', '\x7A')
        {
        }
    }
}