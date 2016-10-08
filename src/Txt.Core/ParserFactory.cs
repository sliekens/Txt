namespace Txt.Core
{
    public abstract class ParserFactory<TElement, TResult> : IParserFactory<TElement, TResult>
        where TElement : Element
    {
        private readonly SingletonParserFactory<TElement, TResult> singleton;

        protected ParserFactory()
        {
            singleton = new SingletonParserFactory<TElement, TResult>(this);
        }

        public abstract IParser<TElement, TResult> Create();

        public IParserFactory<TElement, TResult> Singleton()
        {
            return singleton;
        }
    }
}
