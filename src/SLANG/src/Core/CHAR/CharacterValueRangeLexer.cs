namespace SLANG.Core
{
    public class CharacterValueRangeLexer : ValueRangeLexer
    {
        public CharacterValueRangeLexer()
            : base('\x01', '\x7F')
        {
        }
    }
}