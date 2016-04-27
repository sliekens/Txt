// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndOfLine.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Jetbrains.Annotations;

namespace Txt.ABNF.Core.CRLF
{
    public class EndOfLine : Concatenation
    {
        /// <summary>
        /// </summary>
        /// <param name="concatenation"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="concatenation" /> is a null reference.</exception>
        public EndOfLine([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
