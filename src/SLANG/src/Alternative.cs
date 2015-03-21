namespace SLANG
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents a choice of a range of alternative elements.</summary>
    public class Alternative : Element
    {
        /// <summary>Initializes a new instance of the <see cref="Alternative"/> class with a specified alternative.</summary>
        /// <param name="data">The alternative element.</param>
        /// <param name="lowerBound">The lower bound of the range of alternatives.</param>
        /// <param name="upperBound">The upper bound of the range of alternatives.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Alternative(char data, char lowerBound, char upperBound, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data >= lowerBound && data <= upperBound);
            Contract.Requires(context != null);
        }
    }
}
