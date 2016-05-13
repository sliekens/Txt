using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public sealed class ReadResult<T>
        where T : Element
    {
        private ReadResult([NotNull] SyntaxError error)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error));
            }
            Error = error;
            Text = string.Empty;
            ErrorText = error.ErrorText;
            EndOfInput = error.EndOfInput;
            Success = false;
            Text = error.Text;
        }

        private ReadResult([NotNull] T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            Success = true;
            Text = element.Text;
            ErrorText = string.Empty;
            Element = element;
        }

        public T Element { get; }

        public bool EndOfInput { get; }

        public SyntaxError Error { get; }

        [NotNull]
        public string ErrorText { get; }

        public bool Success { get; }

        [NotNull]
        public string Text { get; }

        public static ReadResult<T> FromResult([NotNull] T result)
        {
            return new ReadResult<T>(result);
        }

        public static ReadResult<T> FromSyntaxError([NotNull] SyntaxError error)
        {
            return new ReadResult<T>(error);
        }
    }
}
