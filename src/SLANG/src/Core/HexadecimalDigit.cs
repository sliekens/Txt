// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    /// <summary>
    /// Represents the HEXDIG rule: 1 hexadecimal digit (case-insensitive). Unicode: U+0030 - U+0039, U+0041 - U+0046,
    /// U+0061 - U+0066.
    /// </summary>
    public class HexadecimalDigit : Alternative<Digit, Element, Element, Element, Element, Element, Element>
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.HexadecimalDigit"/> class with a specified character
        /// and context.</summary>
        /// <param name="element">The hexadecimal digit.</param>
        /// <param name="alternative">A number that indicates which alternative was matched.</param>
        public HexadecimalDigit(Element element, int alternative)
            : base(element, alternative)
        {
            switch (alternative)
            {
                case 1:
                    break;
                case 2:
                    if (element.Data != "A")
                    {
                        throw new ArgumentOutOfRangeException("element", element, "Precondition: element.Data == \"A\"");
                    }

                    break;
                case 3:
                    if (element.Data != "B")
                    {
                        throw new ArgumentOutOfRangeException("element", element, "Precondition: element.Data == \"B\"");
                    }

                    break;
                case 4:
                    if (element.Data != "C")
                    {
                        throw new ArgumentOutOfRangeException("element", element, "Precondition: element.Data == \"C\"");
                    }

                    break;
                case 5:
                    if (element.Data != "D")
                    {
                        throw new ArgumentOutOfRangeException("element", element, "Precondition: element.Data == \"D\"");
                    }

                    break;
                case 6:
                    if (element.Data != "E")
                    {
                        throw new ArgumentOutOfRangeException("element", element, "Precondition: element.Data == \"E\"");
                    }

                    break;
                case 7:
                    if (element.Data != "F")
                    {
                        throw new ArgumentOutOfRangeException("element", element, "Precondition: element.Data == \"F\"");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException("alternative", alternative, "Precondition: 1 <= alternative <= 7");
            }
        }
    }
}
