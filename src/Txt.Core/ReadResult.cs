using JetBrains.Annotations;

namespace Txt.Core
{
    public sealed class ReadResult<T> : IReadResult<T>
        where T : Element
    {
        public static readonly ReadResult<T> None = new ReadResult<T>(false, null, null);

        public ReadResult(bool success, T element, SyntaxError syntaxError)
        {
            IsSuccess = success;
            Element = element;
            SyntaxError = syntaxError;
        }

        public T Element { get; }

        public bool IsSuccess { get; }

        public SyntaxError SyntaxError { get; }

        public static ReadResult<T> Fail(SyntaxError syntaxError)
        {
            return new ReadResult<T>(false, null, syntaxError);
        }

        public static ReadResult<T> Success([NotNull] T element)
        {
            return new ReadResult<T>(true, element, null);
        }
    }
}
