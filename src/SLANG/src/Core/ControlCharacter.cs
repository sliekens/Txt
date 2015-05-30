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
    /// <summary>Represents the CTL rule: 1 control character. Unicode: U+0000 - U+001F, U+007F.</summary>
    public partial class ControlCharacter : Alternative<ControlCharacter.Controls, ControlCharacter.Delete>
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
        public ControlCharacter(Delete element)
            : base(element, 2)
        {
        }
    }

    public partial class ControlCharacter
    {
        public class Controls : Element
        {
            public Controls(Element element)
                : base(element)
            {
            }
        }
    }

    public partial class ControlCharacter
    {
        public class Delete : Element
        {
            public Delete(Element element)
                : base(element)
            {
            }
        }
    }
}