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
                throw new ArgumentOutOfRangeException(nameof(upperBound), "Precondition: upperBound >= lowerBound");
            }

            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        public override ReadResult<Terminal> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }

            var context = scanner.GetContext();
            if (scanner.EndOfInput)
            {
                return ReadResult<Terminal>.FromError(new SyntaxError
                {
                    Message = $"Unexpected end of input. Expected value range: '0x{(int)this.lowerBound:X2}-{(int)this.upperBound:X2}'.",
                    Context = context
                });
            }

            char next = default(char);
            for (var c = this.lowerBound; c <= this.upperBound; c++)
            {
                if (scanner.TryMatch(c, out next))
                {
                    var element = new Terminal(next, context);
                    if (previousElementOrNull != null)
                    {
                        element.PreviousElement = previousElementOrNull;
                        previousElementOrNull.NextElement = element;
                    }

                    return ReadResult<Terminal>.FromResult(element);
                }
            }

            return ReadResult<Terminal>.FromError(new SyntaxError
            {
                Message = $"Unexpected symbol: '{next}'. Expected value range: '0x{(int)this.lowerBound:X2}-{(int)this.upperBound:X2}'.",
                Context = context
            });
        }
    }
}