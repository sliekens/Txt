using Txt.Core;

namespace Txt.ABNF.Core.LF
{
    public class LineFeedParser : Parser<LineFeed, char>
    {
        protected override char ParseImpl(LineFeed value)
        {
            return value.Text[0];
        }
    }
}
