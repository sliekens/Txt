// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Alpha.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;

namespace Txt.ABNF.Core.ALPHA
{
    public class Alpha : Alternation
    {
        public Alpha([NotNull] Alternation element)
            : base(element)
        {
        }
    }
}
