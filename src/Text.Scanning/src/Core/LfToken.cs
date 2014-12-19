namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents an LF character (Line Feed). Unicode: U+000A.</summary>
    public class LfToken : Token
    {
        public LfToken(ITextContext context)
            : base("\u000A", context)
        {
            Contract.Requires(context != null);
        }
    }
}