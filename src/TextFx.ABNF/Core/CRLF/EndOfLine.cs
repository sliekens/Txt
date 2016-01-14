// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndOfLine.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System;
    using JetBrains.Annotations;

    public class EndOfLine : Concatenation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="concatenation"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="concatenation" /> is a null reference.</exception>
        public EndOfLine([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
