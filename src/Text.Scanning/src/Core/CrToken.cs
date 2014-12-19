namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents a CR character (Carriage Return). Unicode: U+000D.</summary>
    public class CrToken : Token
    {
        /// <summary>
        /// Creates a new instance of the <see cref="T:Text.Scanning.Core.CrToken" /> class with a specified character
        /// and context.
        /// </summary>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public CrToken(ITextContext context)
            : base('\u000D', context)
        {
            Contract.Requires(context != null);
        }
    }
}