using JetBrains.Annotations;

namespace Txt.ABNF.Core.CHAR
{
    public class Character : Terminal
    {
        public Character([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
