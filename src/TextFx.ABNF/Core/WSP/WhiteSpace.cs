// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhiteSpace.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using JetBrains.Annotations;

    public class WhiteSpace : Alternative
    {
        public WhiteSpace([NotNull] Alternative element)
            : base(element)
        {
        }
    }
}
