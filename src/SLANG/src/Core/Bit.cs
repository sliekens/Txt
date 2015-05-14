// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    /// <summary>Represents the BIT rule: 0 or 1. Unicode: U+0030 / U+0031.</summary>
    public class Bit : Alternative<Element, Element>
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.Bit"/> class with a specified character
        /// and context.</summary>
        /// <param name="element">The bit.</param>
        /// <param name="alternative">A number that indicates which alternative was matched.</param>
        public Bit(Element element, int alternative)
            : base(element, alternative)
        {
            switch (alternative)
            {
                case 1:
                    if (element.Data != "0")
                    {
                        throw new ArgumentOutOfRangeException("element", element, "Precondition: element.Data == \"1\"");
                    }

                    break;
                case 2:
                    if (element.Data != "1")
                    {
                        throw new ArgumentOutOfRangeException("element", element, "Precondition: element.Data == \"2\"");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException("alternative", alternative, "Precondition: 1 <= alternative <= 2");
            }
        }
    }
}