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
    public class ControlCharacter : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.ControlCharacter"/> class with a specified character
        /// and context.</summary>
        /// <param name="data">The control character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public ControlCharacter(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data <= '\x1F' || data == '\x7F');
            Contract.Requires(context != null);
        }
    }
}