namespace TextFx
{
    using System;
    using JetBrains.Annotations;

    public sealed class SyntaxError
    {
        public SyntaxError(
            bool endOfInput,
            [NotNull] string text,
            [NotNull] string errorText,
            [NotNull] ITextContext context,
            [CanBeNull] SyntaxError innerError = null)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (errorText == null)
            {
                throw new ArgumentNullException(nameof(errorText));
            }
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            EndOfInput = endOfInput;
            Text = text;
            ErrorText = errorText;
            Context = context;
            if (innerError != null)
            {
                InnerError = innerError;
            }
        }

        [NotNull]
        public ITextContext Context { get; }

        public bool EndOfInput { get; }

        [NotNull]
        public string ErrorText { get; }

        [CanBeNull]
        public SyntaxError InnerError { get; }

        [NotNull]
        public string Text { get; }

        public static SyntaxError FromMatchResult([NotNull] MatchResult matchResult, [NotNull] ITextContext context)
        {
            if (matchResult == null)
            {
                throw new ArgumentNullException(nameof(matchResult));
            }
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (matchResult.Success)
            {
                throw new ArgumentException("Argument is not an error result", nameof(matchResult));
            }
            return new SyntaxError(matchResult.EndOfInput, string.Empty, matchResult.Text, context);
        }

        public static SyntaxError FromReadResult<T>([NotNull] ReadResult<T> readResult, [NotNull] ITextContext context)
            where T : Element
        {
            if (readResult == null)
            {
                throw new ArgumentNullException(nameof(readResult));
            }
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (readResult.Success)
            {
                throw new ArgumentException("Argument is not an error result", nameof(readResult));
            }
            return new SyntaxError(
                readResult.EndOfInput,
                readResult.Text,
                readResult.ErrorText,
                context,
                readResult.Error);
        }
    }
}
