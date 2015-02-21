﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Space.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the SP rule: 1 space. Unicode: U+0020.</summary>
    public class Space : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.Core.Space"/> class with a specified context.</summary>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Space(ITextContext context)
            : base('\u0020', context)
        {
            Contract.Requires(context != null);
        }
    }
}