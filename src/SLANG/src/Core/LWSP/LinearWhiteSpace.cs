// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpace.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>Represents the LWSP rule: any linear white space. The LWSP rule permits lines containing only white space.</summary>
    public class LinearWhiteSpace : Repetition
    {
        public LinearWhiteSpace(Repetition element)
            : base(element)
        {
        }
    }
}