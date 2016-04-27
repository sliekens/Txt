// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpace.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Jetbrains.Annotations;

namespace Txt.ABNF.Core.LWSP
{
    public class LinearWhiteSpace : Repetition
    {
        /// <summary>
        /// </summary>
        /// <param name="repetition"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="repetition" /> is a null reference.</exception>
        public LinearWhiteSpace([NotNull] Repetition repetition)
            : base(repetition)
        {
        }

        public override string GetWellFormedText()
        {
            // LWSP is optional, so don't return white space if there was no white space to begin with
            if (Text.Length == 0)
            {
                return string.Empty;
            }

            // Well-formed LWSP is exactly one (1) space
            return " ";
        }
    }
}
