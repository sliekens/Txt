namespace SLANG
{
    using System;
    using System.Diagnostics;

    /// <summary>Provides the base class for lexers whose lexer rule has a range of alternatives.</summary>
    public class ValueRangeLexer : Lexer<Terminal>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly char lowerBound;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly char upperBound;

        /// <summary>Initializes a new instance of the <see cref="ValueRangeLexer"/> class for an unnamed element.</summary>
        /// <param name="lowerBound">The lower bound of the range of alternatives.</param>
        /// <param name="upperBound">The upper bound of the range of alternatives.</param>
        public ValueRangeLexer(char lowerBound, char upperBound)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Terminal element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner", "Precondition: scanner != null");
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
                    return true;
                }
            }

            element = default(Terminal);
            return false;
        }
    }
}
