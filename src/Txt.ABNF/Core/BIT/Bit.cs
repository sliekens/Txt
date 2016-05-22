// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;

namespace Txt.ABNF.Core.BIT
{
    public class Bit : Alternation
    {
        public Bit([NotNull] Alternation element)
            : base(element)
        {
        }
    }
}
