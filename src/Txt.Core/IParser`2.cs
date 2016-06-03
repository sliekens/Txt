using JetBrains.Annotations;

namespace Txt.Core
{
    public interface IParser<in TElement, out TResult>
        where TElement : Element
    {
        [NotNull]
        TResult Parse([NotNull] TElement value);
    }
}
