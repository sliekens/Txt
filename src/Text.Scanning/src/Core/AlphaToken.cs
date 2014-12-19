namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents a letter of the alphabet (case-insensitive). Unicode: U+0041 - U+005A, U+0061 - U+007A.</summary>
    public class AlphaToken : Token
    {
        public AlphaToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires((data >= '\u0041' && data <= '\u005A') || (data >= '\u0061' && data <= '\u007A'));
            Contract.Requires(context != null);
        }
    }
}