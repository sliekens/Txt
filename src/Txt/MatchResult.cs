using System;
using Jetbrains.Annotations;

namespace Txt
{
    public sealed class MatchResult
    {
        private MatchResult(bool endOfInput, bool success, [NotNull] string text, [NotNull] string expected)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }
            EndOfInput = endOfInput;
            Success = success;
            Text = text;
            Expected = expected;
        }

        /// <summary>Gets a value indicating whether the end of input was reached.</summary>
        public bool EndOfInput { get; }

        /// <summary>
        ///     Gets the text that was expected. If <see cref="Success" /> is <c>true</c> then the expected text and the
        ///     matched text can still be different, depending on the rules of the original comparison.
        /// </summary>
        public string Expected { get; }

        /// <summary>Gets a value indicating whether there was a match.</summary>
        public bool Success { get; }

        /// <summary>Gets the text that was matched.</summary>
        public string Text { get; }

        [NotNull]
        public static MatchResult FromEndOfInput([NotNull] string expected)
        {
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }
            return new MatchResult(true, false, string.Empty, expected);
        }

        [NotNull]
        public static MatchResult FromMatch([NotNull] string text, [NotNull] string expected)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }
            return new MatchResult(false, true, text, expected);
        }

        public static MatchResult FromMismatch([NotNull] string text, [NotNull] string expected)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }
            return new MatchResult(false, false, text, expected);
        }
    }
}
