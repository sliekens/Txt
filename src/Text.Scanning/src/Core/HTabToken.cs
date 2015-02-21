namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the HTAB rule: 1 horizontal tab. Unicode: U+0009.</summary>
    public class HTabToken : Token
    {
        /// <summary>Creates a new instance of the <see cref="T:Text.Scanning.Core.HTabToken" /> class with a specified context.</summary>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HTabToken(ITextContext context)
            : base("\u0009", context)
        {
            Contract.Requires(context != null);
        }
    }
}