﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CarriageReturn.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the CR rule: 1 carriage return. Unicode: U+000D.</summary>
    public class CarriageReturn : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.CarriageReturn"/> class with a specified context.</summary>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public CarriageReturn(ITextContext context)
            : base('\x0D', context)
        {
            Contract.Requires(context != null);
        }
    }
}