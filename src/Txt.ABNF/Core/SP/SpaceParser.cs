using Txt.Core;

namespace Txt.ABNF.Core.SP
{
    public class SpaceParser : Parser<Space, char>
    {
        protected override char ParseImpl(Space value)
        {
            return value.Text[0];
        }
    }
}
