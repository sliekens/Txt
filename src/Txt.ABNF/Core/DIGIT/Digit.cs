using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.DIGIT
{
    public class Digit : Terminal
    {
        public Digit([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
