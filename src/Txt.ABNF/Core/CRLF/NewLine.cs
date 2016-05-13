﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewLine.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using JetBrains.Annotations;

namespace Txt.ABNF.Core.CRLF
{
    public class NewLine : Concatenation
    {
        /// <summary>
        /// </summary>
        /// <param name="concatenation"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="concatenation" /> is a null reference.</exception>
        public NewLine([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
