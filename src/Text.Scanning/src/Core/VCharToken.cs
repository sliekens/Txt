namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents a visible (printing) char. Unicode: U+0021 - U+007E.</summary>
    public class VCharToken : Token
    {
        /// <summary>
        /// Creates a new instance of the <see cref="T:Text.Scanning.Core.VCharToken" /> class with a specified character
        /// and context.
        /// </summary>
        /// <param name="data">The VCHAR character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public VCharToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data >= '\u0021' && data <= '\u007E');
            Contract.Requires(context != null);
        }
    }
}