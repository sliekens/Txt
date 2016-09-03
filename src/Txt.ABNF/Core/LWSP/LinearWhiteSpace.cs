using System;
using JetBrains.Annotations;

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
    }
}
