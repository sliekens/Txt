namespace Txt.Core
{
    public interface IReadResult<out T>
        where T : Element
    {
        T Element { get; }

        bool EndOfInput { get; }

        SyntaxError Error { get; }

        string ErrorText { get; }

        bool Success { get; }

        string Text { get; }
    }
}
