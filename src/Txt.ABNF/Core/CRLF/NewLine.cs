using JetBrains.Annotations;

namespace Txt.ABNF.Core.CRLF
{
    public class NewLine : Concatenation
    {
        public NewLine([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
