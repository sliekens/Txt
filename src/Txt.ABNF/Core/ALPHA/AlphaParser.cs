using Txt.Core;

namespace Txt.ABNF.Core.ALPHA
{
    public class AlphaParser : Parser<Alpha, char>
    {
        protected override char ParseImpl(Alpha value)
        {
            return value.Text[0];
        }
    }
}
