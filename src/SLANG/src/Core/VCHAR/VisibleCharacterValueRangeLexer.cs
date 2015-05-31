namespace SLANG.Core
{
    public class VisibleCharacterValueRangeLexer : ValueRangeLexer
    {
        public VisibleCharacterValueRangeLexer()
            : base('\x21', '\x7E')
        {
        }
    }
}