using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.CR
{
    public class CarriageReturn : Terminal
    {
        public CarriageReturn([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
