// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Octet.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.OCTET
{
    public class Octet : Element
    {
        /// <summary>Initializes a new instance of the <see cref="Element" /> class with a given element to copy.</summary>
        /// <param name="element">The element to copy.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="element" /> is a null reference.</exception>
        public Octet([NotNull] Element element)
            : base(element)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Element" /> class with a given string of terminal values and its
        ///     context.
        /// </summary>
        /// <param name="terminals">The terminal values.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        /// <exception cref="ArgumentNullException">
        ///     The value of  <paramref name="terminals" /> or <paramref name="context" /> is a
        ///     null reference.
        /// </exception>
        public Octet(int value, [NotNull] ITextContext context)
            : base("?", context)
        {
            Value = value;
        }

        public int Value { get; }
    }
}
