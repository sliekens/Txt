using Txt.Core;

namespace Txt.ABNF.Core.CHAR
{
    public class CharacterParser : Parser<Character, char>
    {
        protected override char ParseImpl(Character value)
        {
            return value.Text[0];
        }
    }
}
