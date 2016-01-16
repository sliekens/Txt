namespace TextFx.ABNF
{
    using System;
    using System.Diagnostics;
    using JetBrains.Annotations;

    /// <summary>Provides methods for reading a range of alternative values.</summary>
    public class ValueRangeLexer : Lexer<Terminal>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly int lowerBound;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly int upperBound;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly char[] valueRange;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValueRangeLexer" /> class with a specified value range.
        /// </summary>
        public ValueRangeLexer([NotNull] char[] valueRange, int lowerBound, int upperBound)
        {
            if (valueRange == null)
            {
                throw new ArgumentNullException(nameof(valueRange));
            }
            if (valueRange.Length == 0)
            {
                throw new ArgumentException("Argument is empty collection", nameof(valueRange));
            }
            if (upperBound < lowerBound)
            {
                throw new ArgumentOutOfRangeException(nameof(upperBound), "Precondition: upperBound >= lowerBound");
            }
            this.valueRange = valueRange;
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        public override ReadResult<Terminal> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var context = scanner.GetContext();
            if (scanner.EndOfInput)
            {
                return ReadResult<Terminal>.FromError(
                    new SyntaxError
                    {
                        Message = $"Unexpected end of input. Expected value range: '0x{lowerBound:X2}-{upperBound:X2}'.",
                        Context = context
                    });
            }
            MatchResult result = null;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < valueRange.Length; i++)
            {
                var c = valueRange[i];
                result = scanner.TryMatch(c);
                if (!result.Success)
                {
                    continue;
                }
                return ReadResult<Terminal>.FromResult(new Terminal(result.Text, context));
            }
            Debug.Assert(result != null, "result != null");
            return ReadResult<Terminal>.FromError(
                new SyntaxError
                {
                    Message =
                        $"Unexpected symbol: '{result.Text}'. Expected value range: '0x{lowerBound:X2}-{upperBound:X2}'.",
                    Context = context
                });
        }
    }
}
