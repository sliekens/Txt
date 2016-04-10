// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Alpha.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Jetbrains.Annotations;

namespace Text.ABNF.Core.ALPHA
{
    public class Alpha : Alternative
    {
        public Alpha([NotNull] Alternative element)
            : base(element)
        {
        }
    }
}
