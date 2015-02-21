namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the DIGIT rule: 1 digit. Unicode: U+0030 - U+0039.</summary>
    public class DigitElement : Element
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Text.Scanning.Core.DigitElement" /> class with a specified character
        /// and context.
        /// </summary>
        /// <param name="data">The digit.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public DigitElement(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data >= '\u0030' && data <= '\u0039');
            Contract.Requires(context != null);
        }
    }
}