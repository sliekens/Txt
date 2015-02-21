namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the LF rule: 1 line feed. Unicode: U+000A.</summary>
    public class LfElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.Core.LfElement" /> class with a specified context.</summary>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public LfElement(ITextContext context)
            : base("\u000A", context)
        {
            Contract.Requires(context != null);
        }
    }
}