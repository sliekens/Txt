namespace SLANG.Core
{
    public class OctetValueRangeLexer : ValueRangeLexer
    {
        public OctetValueRangeLexer()
            : base('\x00', '\xFF')
        {
        }
    }
}