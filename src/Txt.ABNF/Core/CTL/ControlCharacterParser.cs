using Txt.Core;

namespace Txt.ABNF.Core.CTL
{
    public class ControlCharacterParser : Parser<ControlCharacter, char>
    {
        protected override char ParseImpl(ControlCharacter value)
        {
            return value.Text[0];
        }
    }
}
