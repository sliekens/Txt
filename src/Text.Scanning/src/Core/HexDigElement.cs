namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Represents the HEXDIG rule: 1 hexadecimal digit (case-insensitive). Unicode: U+0030 - U+0039, U+0041 - U+0046,
    /// U+0061 - U+0066.
    /// </summary>
    public class HexDigElement : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DigitElement digitElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Text.Scanning.Core.HexDigElement" /> class with a specified character
        /// and context.
        /// </summary>
        /// <param name="digitElement">The digit.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HexDigElement(DigitElement digitElement, ITextContext context)
            : base(digitElement.Data, context)
        {
            Contract.Requires(digitElement != null);
            Contract.Requires(context != null);
            this.digitElement = digitElement;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Text.Scanning.Core.HexDigElement" /> class with a specified character
        /// and context.
        /// </summary>
        /// <param name="data">The hexadecimal digit.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HexDigElement(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires((data >= '\u0030' && data <= '\u0039') || (data >= '\u0041' && data <= '\u0046') ||
                              (data >= '\u0061' && data <= '\u0066'));
            Contract.Requires(context != null);
            if (data <= '\u0039')
            {
                this.digitElement = new DigitElement(data, context);
            }
        }

        /// <summary>Gets the 'DIGIT' component, or a <c>null</c> reference.</summary>
        public DigitElement DigitElement
        {
            get
            {
                return this.digitElement;
            }
        }
    }
}