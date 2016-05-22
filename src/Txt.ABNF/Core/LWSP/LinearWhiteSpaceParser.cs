using Txt.Core;

namespace Txt.ABNF.Core.LWSP
{
    public class LinearWhiteSpaceParser : Parser<LinearWhiteSpace, string>
    {
        protected override string ParseImpl(LinearWhiteSpace value)
        {
            // Well-formed LWSP is exactly one (1) space
            // LWSP is optional, so don't return white space if there was no white space to begin with
            return value.Elements.Count == 0 ? "" : " ";
        }
    }
}
