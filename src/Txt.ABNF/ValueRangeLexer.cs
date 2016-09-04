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

        protected override IReadResult<Terminal> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            if (scanner.Peek() == -1)
            {
                return ReadResult<Terminal>.Fail(new SyntaxError(context, "Unexpected end of input"));
            }

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < valueRange.Length; i++)
            {
                var c = valueRange[i];
                var result = scanner.TryMatch(c);
                if (result.IsMatch)
                {
                    var terminal = new Terminal(result.Text, context);
                    return ReadResult<Terminal>.Success(terminal);
                }
            }
            return ReadResult<Terminal>.None;
        }
    }
}
