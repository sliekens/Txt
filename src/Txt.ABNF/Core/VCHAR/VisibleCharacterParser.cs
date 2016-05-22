using Txt.Core;

namespace Txt.ABNF.Core.VCHAR
{
    public class VisibleCharacterParser : Parser<VisibleCharacter, char>
    {
        protected override char ParseImpl(VisibleCharacter value)
        {
            return value.Text[0];
        }
    }
}
