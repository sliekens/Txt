namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the VCHAR rule: 1 visible (printing) character. Unicode: U+0041 - U+005A, U+0061 - U+007A.</summary>
    public class VCharElement : Element
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Text.Scanning.Core.VCharElement" /> class with a specified character
        /// and context.
        /// </summary>
        /// <param name="data">The 'VCHAR' character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public VCharElement(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data >= '\u0021' && data <= '\u007E');
            Contract.Requires(context != null);
        }
    }
}