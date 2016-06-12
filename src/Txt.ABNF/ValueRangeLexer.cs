using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>Provides methods for reading a range of alternative values.</summary>
    public class ValueRangeLexer : Lexer<Terminal>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly char[] valueRange;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValueRangeLexer" /> class with a specified value range.
        /// </summary>
        public ValueRangeLexer([NotNull] char[] valueRange)
        {
            if (valueRange == null)
            {
                throw new ArgumentNullException(nameof(valueRange));
            }
            if (valueRange.Length == 0)
            {
                throw new ArgumentException("Argument is empty collection", nameof(valueRange));
            }
            this.valueRange = valueRange;
        }

        protected override ReadResult<Terminal> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            MatchResult result = null;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < valueRange.Length; i++)
            {
                var c = valueRange[i];
                result = scanner.TryMatch(c);
                if (result.EndOfInput)
                {
                    return ReadResult<Terminal>.FromSyntaxError(SyntaxError.FromMatchResult(result, context));
                }
                if (result.Success)
                {
                    return ReadResult<Terminal>.FromResult(new Terminal(result.Text, context));
                }
            }
            Debug.Assert(result != null, "result != null");
            return ReadResult<Terminal>.FromSyntaxError(SyntaxError.FromMatchResult(result, context));
        }
    }
}
