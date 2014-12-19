namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents a CTL character (Control). Unicode: U+0000 - U+001F, U+007F.</summary>
    public class CtlToken : Token
    {
        /// <summary>
        /// Creates a new instance of the <see cref="T:Text.Scanning.Core.CtlToken" /> class with a specified character
        /// and context.
        /// </summary>
        /// <param name="data">The 'CTL' character.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public CtlToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data <= '\u001F' || data == '\u007F');
            Contract.Requires(context != null);
        }
    }
}