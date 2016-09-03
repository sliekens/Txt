using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.LWSP
{
    public class LinearWhiteSpaceLexer : CompositeLexer<Repetition, LinearWhiteSpace>
    {
        public LinearWhiteSpaceLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
