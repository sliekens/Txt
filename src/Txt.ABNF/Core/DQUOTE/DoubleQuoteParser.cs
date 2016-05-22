using Txt.Core;

namespace Txt.ABNF.Core.DQUOTE
{
    public class DoubleQuoteParser : Parser<DoubleQuote, char>
    {
        protected override char ParseImpl(DoubleQuote value)
        {
            return value.Text[0];
        }
    }
}
