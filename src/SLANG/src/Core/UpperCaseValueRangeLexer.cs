namespace SLANG.Core
{
    public class UpperCaseValueRangeLexer : ValueRangeLexer
    {
        public UpperCaseValueRangeLexer()
            : base('\x41', '\x5A')
        {
        }
    }
}