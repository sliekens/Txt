// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Alpha.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using JetBrains.Annotations;

    public class Alpha : Alternative
    {
        public Alpha([NotNull] Alternative element)
            : base(element)
        {
        }
    }
}
