// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexDigToken.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Represents the HEXDIG rule: 1 hexadecimal digit (case-insensitive). Unicode: U+0030 - U+0039, U+0041 - U+0046,
//   U+0061 - U+0066.
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
    public class HexDigToken : Token
    {
        /// <summary>The digit token.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DigitToken digitToken;

        /// <summary>Initializes a new instance of the <see cref="HexDigToken"/> class. Creates a new instance of the <see cref="T:Text.Scanning.Core.HexDigToken"/> class with a specified character
        /// and context.</summary>
        /// <param name="digitToken">The digit.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HexDigToken(DigitToken digitToken, ITextContext context)
            : base(digitToken.Data, context)
        {
            Contract.Requires(digitToken != null);
            Contract.Requires(context != null);
            this.digitToken = digitToken;
        }

        /// <summary>Initializes a new instance of the <see cref="HexDigToken"/> class. Creates a new instance of the <see cref="T:Text.Scanning.Core.HexDigToken"/> class with a specified character
        /// and context.</summary>
        /// <param name="data">The hexadecimal digit.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HexDigToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(
                (data >= '\u0030' && data <= '\u0039') || (data >= '\u0041' && data <= '\u0046')
                || (data >= '\u0061' && data <= '\u0066'));
            Contract.Requires(context != null);
            if (data <= '\u0039')
            {
                this.digitToken = new DigitToken(data, context);
            }
        }

        /// <summary>Gets the 'DIGIT' component, or a <c>null</c> reference.</summary>
        public DigitToken DigitToken
        {
            get
            {
                return this.digitToken;
            }
        }
    }
}