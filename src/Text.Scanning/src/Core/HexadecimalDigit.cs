// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Represents the HEXDIG rule: 1 hexadecimal digit (case-insensitive). Unicode: U+0030 - U+0039, U+0041 - U+0046,
    /// U+0061 - U+0066.
    /// </summary>
    public class HexadecimalDigit : Element
    {
        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Digit digit;

        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.Core.HexadecimalDigit"/> class with a specified character
        /// and context.</summary>
        /// <param name="digit">The digit.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HexadecimalDigit(Digit digit, ITextContext context)
            : base(digit.Data, context)
        {
            Contract.Requires(digit != null);
            Contract.Requires(context != null);
            this.digit = digit;
        }

        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.Core.HexadecimalDigit"/> class with a specified character
        /// and context.</summary>
        /// <param name="data">The hexadecimal digit.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HexadecimalDigit(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(
                (data >= '\u0030' && data <= '\u0039') || (data >= '\u0041' && data <= '\u0046')
                || (data >= '\u0061' && data <= '\u0066'));
            Contract.Requires(context != null);
            if (data <= '\u0039')
            {
                this.digit = new Digit(data, context);
            }
        }

        /// <summary>Gets the 'DIGIT' component, or a <c>null</c> reference.</summary>
        public Digit Digit
        {
            get
            {
                return this.digit;
            }
        }
    }
}