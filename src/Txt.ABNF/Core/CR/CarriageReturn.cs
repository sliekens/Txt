using JetBrains.Annotations;

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
