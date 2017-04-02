using JetBrains.Annotations;

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
