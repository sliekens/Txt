// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhiteSpace.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Jetbrains.Annotations;

namespace Text.ABNF.Core.WSP
{
    public class WhiteSpace : Alternative
    {
        public WhiteSpace([NotNull] Alternative element)
            : base(element)
        {
        }
    }
}
