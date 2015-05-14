// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCharacter.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    /// <summary>Represents the CTL rule: 1 control character. Unicode: U+0000 - U+001F, U+007F.</summary>
    public partial class ControlCharacter : Alternative<ControlCharacter.Controls, Element>
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.ControlCharacter"/> class with a specified character
        /// and context.</summary>
        /// <param name="element">The control character.</param>
        public ControlCharacter(Controls element)
            : base(element, 1)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.ControlCharacter"/> class with a specified character
        /// and context.</summary>
        /// <param name="element">The control character.</param>
        public ControlCharacter(Element element)
            : base(element, 2)
        {
            if (element.Data != "\x7F")
            {
                throw new ArgumentOutOfRangeException("element", element, "Precondition: element.Data == \"\\x7F\"");
            }
        }
    }

    public partial class ControlCharacter
    {
        public class Controls : Alternative
        {
            public Controls(Element element)
                : base(element, '\0', '\x1F')
            {
            }
        }
    }
}