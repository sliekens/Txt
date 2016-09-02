using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Txt.Core
{
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

        public IList<SyntaxError> MakeFlat()
        {
            var result = new List<SyntaxError>(1);
            var current = this;
            while (current != null)
            {
                result.Add(this);
                current = current.InnerError;
            }

            return result;
        }

        public SyntaxError GetBaseError()
        {
            var current = this;
            while (current.InnerError != null)
            {
                current = current.InnerError;
            }

            return current;
        }

        public static SyntaxError FromMatchResult([NotNull] ScanResult scanResult, [NotNull] ITextContext context)
        {
            if (scanResult == null)
            {
                throw new ArgumentNullException(nameof(scanResult));
            }
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (scanResult.Success)
            {
                throw new ArgumentException("Argument is not an error result", nameof(scanResult));
            }
            return new SyntaxError(scanResult.EndOfInput, string.Empty, scanResult.Text, context);
        }

        public static SyntaxError FromReadResult<T>([NotNull] IReadResult<T> readResult, [NotNull] ITextContext context)
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
