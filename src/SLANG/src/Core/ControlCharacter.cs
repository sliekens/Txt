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
    using System.Diagnostics.Contracts;

    /// <summary>Represents the CTL rule: 1 control character. Unicode: U+0000 - U+001F, U+007F.</summary>
    public partial class ControlCharacter : Alternative<ControlCharacter.Controls, Element>
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.ControlCharacter"/> class with a specified character
        /// and context.</summary>
        /// <param name="element">The control character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public ControlCharacter(Controls element, ITextContext context)
            : base(element, 1, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }

        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.ControlCharacter"/> class with a specified character
        /// and context.</summary>
        /// <param name="element">The control character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public ControlCharacter(Element element, ITextContext context)
            : base(element, 2, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }
    }

    public partial class ControlCharacter
    {
        public class Controls : Alternative
        {
            public Controls(char data, ITextContext context)
                : base(data, '\0', '\x1F', context)
            {
            }
        }
    }
}