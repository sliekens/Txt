using System;
using System.Text;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>Creates instances of the <see cref="ValueRangeLexer" /> class.</summary>
    public class ValueRangeLexerFactory : IValueRangeLexerFactory
    {
        [NotNull]
        public static ValueRangeLexerFactory Default { get; } = new ValueRangeLexerFactory();

        /// <summary>
        ///     Initializes a new instance of a class that implements the <see cref="ILexer{TElement}" /> interface with a
        ///     specified lower and upper bound, both inclusive.
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range of alternatives (inclusive).</param>
        /// <param name="upperBound">The upper bound of the range of alternatives (inclusive).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The value of <paramref name="upperBound" /> is smaller than the value of
        ///     <paramref name="lowerBound" />.
        /// </exception>
        public ILexer<Terminal> Create(char lowerBound, char upperBound)
        {
            if (upperBound < lowerBound)
            {
                throw new ArgumentOutOfRangeException(nameof(upperBound), "Precondition: upperBound >= lowerBound");
            }
            var range = new char[1 + upperBound - lowerBound];
            int i;
            char c;
            for (i = 0, c = lowerBound; c <= upperBound; i++, c++)
            {
                range[i] = c;
            }
            return new ValueRangeLexer(range);
        }

        /// <summary>
        ///     Initializes a new instance of a class that implements the <see cref="ILexer{TElement}" /> interface with a
        ///     specified lower and upper bound, both inclusive.
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range of alternatives (inclusive).</param>
        /// <param name="upperBound">The upper bound of the range of alternatives (inclusive).</param>
        /// <param name="encoding">The encoding that is used to convert from <see cref="int" /> to <see cref="char" />.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="encoding" /> is a null reference.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The value of <paramref name="upperBound" /> is smaller than the value of
        ///     <paramref name="lowerBound" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     One or more values in the range are not valid in the specified encoding.
        /// </exception>
        public ILexer<Terminal> Create(int lowerBound, int upperBound, Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }
            if (upperBound < lowerBound)
            {
                throw new ArgumentOutOfRangeException(nameof(upperBound), "Precondition: upperBound >= lowerBound");
            }
            var range = new char[1 + upperBound - lowerBound];
            int i;
            int c;
            for (i = 0, c = lowerBound; c <= upperBound; i++, c++)
            {
                var bytes = BitConverter.GetBytes(c);
                if (!(encoding is UnicodeEncoding))
                {
                    bytes = Encoding.Convert(encoding, Encoding.Unicode, bytes);
                }
                var values = Encoding.Unicode.GetChars(bytes);
                range[i] = values[0];
            }
            return new ValueRangeLexer(range);
        }
    }
}
