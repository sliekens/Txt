namespace Txt.Core
{
    public interface IReadResult<out T>
        where T : Element
    {
        T Element { get; }

        bool IsSuccess { get; }

        SyntaxError SyntaxError { get; }
    }
}
