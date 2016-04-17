// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhiteSpace.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Jetbrains.Annotations;

namespace Txt.ABNF.Core.WSP
{
    public class WhiteSpace : Alternation
    {
        public WhiteSpace([NotNull] Alternation element)
            : base(element)
        {
        }
    }
}
