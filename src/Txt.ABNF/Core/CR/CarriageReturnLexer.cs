// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CarriageReturnLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
