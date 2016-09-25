namespace Txt.Core
{
    public interface IWalker<in TElement>
        where TElement : Element
    {
        void Enter(TElement element);

        void Exit(TElement element);

        /// <summary>Evaluates a given element. A return value indicates whether its descendant nodes should be evaluated as well.</summary>
        /// <param name="element">The element to evaluate.</param>
        /// <returns><c>true</c> if descendant nodes should be evaluated; otherwise, <c>false</c>.</returns>
        bool Walk(TElement element);
    }
}
