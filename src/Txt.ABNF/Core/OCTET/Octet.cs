// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Octet.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Jetbrains.Annotations;

namespace Text.ABNF.Core.OCTET
{
    public class Octet : Terminal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="terminal"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="terminal" /> is a null reference.</exception>
        public Octet([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
