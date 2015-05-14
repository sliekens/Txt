namespace SLANG
{
    using System;
    using System.Linq;

    /// <summary>Represents a choice of a range of alternative elements.</summary>
    public class Alternative : Element
    {
        /// <summary>Initializes a new instance of the <see cref="Alternative"/> class with a specified alternative.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="lowerBound">The lower bound of the range of alternatives.</param>
        /// <param name="upperBound">The upper bound of the range of alternatives.</param>
        public Alternative(Element element, char lowerBound, char upperBound)
            : base(element)
        {
            if (element.Data.Length != 1)
            {
                throw new ArgumentOutOfRangeException("element", "Precondition: element.Data.Length == 1");
            }

            if (lowerBound >= upperBound)
            {
                throw new ArgumentException("Precondition: lowerBound < upperBound");
            }

            for (char c = lowerBound; c < upperBound; c++)
            {
                if (element.Data == char.ToString(c))
                {
                    return;
                }
            }

            throw new ArgumentOutOfRangeException("element", "Precondition: lowerBound <= element.Data <= upperBound");
        }
    }
}
