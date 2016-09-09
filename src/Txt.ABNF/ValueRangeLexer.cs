using System;
using System.Collections.Generic;
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

        public override IEnumerable<Terminal> Read2Impl(ITextScanner scanner, ITextContext context)
        {
            if (scanner.Peek() == -1)
            {
                return Empty;
            }

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < valueRange.Length; i++)
            {
                var c = valueRange[i];
                var result = scanner.TryMatch(c);
                if (result.IsMatch)
                {
                    return new[] { new Terminal(result.Text, context) };
                }
            }
            return Empty;
        }
    }
}
