// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisibleCharacter.cs" company="Steven Liekens">
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

    public class VisibleCharacter : Terminal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="terminal"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="terminal" /> is a null reference.</exception>
        public VisibleCharacter([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
