using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CR
{
    public class CarriageReturnLexer : CompositeLexer<Terminal, CarriageReturn>
    {
        public CarriageReturnLexer([NotNull] ILexer<Terminal> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
