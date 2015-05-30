namespace SLANG.Core
{
    public class ControlsValueRangeLexer : ValueRangeLexer
    {
        public ControlsValueRangeLexer()
            : base('\x00', '\x1F')
        {
        }
    }
}