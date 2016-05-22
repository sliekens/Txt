using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public abstract class Parser<TElement, TResult> : IParser<TElement, TResult>
        where TElement : Element
    {
        public TResult Parse(TElement value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            return ParseImpl(value);
        }

        [NotNull]
        protected abstract TResult ParseImpl([NotNull] TElement value);
    }
}
