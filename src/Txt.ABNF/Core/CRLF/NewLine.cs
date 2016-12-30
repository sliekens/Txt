using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

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
