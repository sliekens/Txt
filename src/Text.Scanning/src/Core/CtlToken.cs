// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CtlToken.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents the CTL rule: 1 control character. Unicode: U+0000 - U+001F, U+007F.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the CTL rule: 1 control character. Unicode: U+0000 - U+001F, U+007F.</summary>
    public class CtlToken : Token
    {
        /// <summary>Initializes a new instance of the <see cref="CtlToken"/> class. Creates a new instance of the <see cref="T:Text.Scanning.Core.CtlToken"/> class with a specified character
        /// and context.</summary>
        /// <param name="data">The control character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public CtlToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data <= '\u001F' || data == '\u007F');
            Contract.Requires(context != null);
        }
    }
}