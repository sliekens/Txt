using JetBrains.Annotations;

namespace Txt.Core
{
    public sealed class ReadResult<T> : IReadResult<T>
        where T : Element
    {
        public static readonly ReadResult<T> None = new ReadResult<T>(false, null);

        public ReadResult(bool success, T element)
        {
            IsSuccess = success;
            Element = element;
        }

        public T Element { get; }

        public bool IsSuccess { get; }

        public static ReadResult<T> Success([NotNull] T element)
        {
            return new ReadResult<T>(true, element);
        }
    }
}
