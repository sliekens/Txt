using Txt.Core;

namespace Txt.ABNF.Core.CRLF
{
    public class NewLineParser : Parser<NewLine, string>
    {
        protected override string ParseImpl(NewLine value)
        {
            return value.Text;
        }
    }
}
