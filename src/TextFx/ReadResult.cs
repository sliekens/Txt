namespace TextFx
{
    public class ReadResult<T>
        where T : Element
    {
        public SyntaxError Error { get; set; }

        public T Element { get; set; }

        public bool Success { get; set; }

        public static ReadResult<T> FromResult(T result)
        {
            return new ReadResult<T>
            {
                Success = true,
                Element = result
            };
        }

        public static ReadResult<T> FromError(SyntaxError error)
        {
            return new ReadResult<T>
            {
                Success = false,
                Error = error
            };
        }
    }
}