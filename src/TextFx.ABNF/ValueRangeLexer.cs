namespace TextFx.ABNF
{
    using System;
    using System.Diagnostics;

    /// <summary>Provides methods for reading a range of alternative values.</summary>
    public class ValueRangeLexer : Lexer<Terminal>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly char lowerBound;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly char upperBound;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValueRangeLexer" /> class with a specified lower and upper bound,
        ///     both inclusive.
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range of alternatives (inclusive).</param>
        /// <param name="upperBound">The upper bound of the range of alternatives (inclusive).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The value of <paramref name="upperBound" /> is smaller than the value of
        ///     <paramref name="lowerBound" />.
        /// </exception>
        public ValueRangeLexer(char lowerBound, char upperBound)
        {
            if (upperBound < lowerBound)
            {
                throw new ArgumentOutOfRangeException("upperBound", "Precondition: upperBound >= lowerBound");
            }

            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Terminal element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner");
            }

            var context = scanner.GetContext();
            for (var c = this.lowerBound; c <= this.upperBound; c++)
            {
                if (scanner.EndOfInput)
                {
                    break;
                }

                if (scanner.TryMatch(c))
                {
                    element = new Terminal(c, context);
                    if (previousElementOrNull != null)
                    {
                        element.PreviousElement = previousElementOrNull;
                        previousElementOrNull.NextElement = element;
                    }

                    return true;
                }
            }

            element = default(Terminal);
            return false;
        }
    }
}