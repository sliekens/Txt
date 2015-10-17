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
    using System.Diagnostics;

    public class Alpha : Alternative
    {
        public Alpha(Alternative element)
            : base(element)
        {
        }
    }
}