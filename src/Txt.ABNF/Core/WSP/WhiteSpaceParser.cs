using Txt.Core;

namespace Txt.ABNF.Core.WSP
{
    public class WhiteSpaceParser : Parser<WhiteSpace, char>
    {
        protected override char ParseImpl(WhiteSpace value)
        {
            return value.Text[0];
        }
    }
}
