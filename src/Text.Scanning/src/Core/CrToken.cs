namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the CR rule: 1 carriage return. Unicode: U+000D.</summary>
    public class CrToken : Token
    {
        /// <summary>Creates a new instance of the <see cref="T:Text.Scanning.Core.CrToken" /> class with a specified context.</summary>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public CrToken(ITextContext context)
            : base('\u000D', context)
        {
            Contract.Requires(context != null);
        }
    }
}