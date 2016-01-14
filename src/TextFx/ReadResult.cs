namespace TextFx
{
    public class ReadResult<T>
        where T : Element
    {
        public T Element { get; set; }

        public SyntaxError Error { get; set; }

        public bool Success { get; set; }

        public static ReadResult<T> FromError(SyntaxError error)
        {
            return new ReadResult<T>
            {
                Success = false,
                Error = error
            };
        }

        public static ReadResult<T> FromResult(T result)
        {
            return new ReadResult<T>
            {
                Success = true,
                Element = result
            };
        }
    }
}
