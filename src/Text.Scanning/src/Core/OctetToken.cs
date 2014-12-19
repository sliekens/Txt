namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the OCTET rule: 8 bits of data. Unicode: U+0000 - U+00FF.</summary>
    public class OctetToken : Token
    {
        /// <summary>
        /// Creates a new instance of the <see cref="T:Text.Scanning.Core.OctetToken" /> class with a specified character
        /// and context.
        /// </summary>
        /// <param name="data">The octet.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public OctetToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data <= (char)0xFF);
            Contract.Requires(context != null);
        }
    }
}