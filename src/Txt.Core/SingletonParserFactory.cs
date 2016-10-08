using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public class SingletonParserFactory<TElement, TResult> : IParserFactory<TElement, TResult>
        where TElement : Element
    {
        [NotNull]
        [ItemNotNull]
        private readonly Lazy<IParser<TElement, TResult>> lazy;

        public SingletonParserFactory([NotNull] IParserFactory<TElement, TResult> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            Factory = factory;
            lazy = new Lazy<IParser<TElement, TResult>>(factory.Create);
        }

        public IParserFactory<TElement, TResult> Factory { get; }

        public IParser<TElement, TResult> Create()
        {
            return lazy.Value;
        }

        public IParserFactory<TElement, TResult> Singleton()
        {
            return this;
        }
    }
}
