using JetBrains.Annotations;

namespace Txt.Core
{
    public interface IParserFactory<in TElement, out TResult>
        where TElement : Element
    {
        [NotNull]
        IParser<TElement, TResult> Create();

        [NotNull]
        IParserFactory<TElement, TResult> Singleton();
    }
}
