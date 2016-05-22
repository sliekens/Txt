using Txt.Core;

namespace Txt.ABNF.Core.HTAB
{
    public class HorizontalTabParser : Parser<HorizontalTab, char>
    {
        protected override char ParseImpl(HorizontalTab value)
        {
            return value.Text[0];
        }
    }
}
