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
    using System.Collections.Generic;

    /// <summary>Represents the LWSP rule: any linear white space. The LWSP rule permits lines containing only white space.</summary>
    public class LinearWhiteSpace : Repetition<Alternative<WhiteSpace, Sequence<EndOfLine, WhiteSpace>>>
    {
        public LinearWhiteSpace(IList<Alternative<WhiteSpace, Sequence<EndOfLine, WhiteSpace>>> elements, ITextContext context)
            : base(elements, context)
        {
        }
    }
}