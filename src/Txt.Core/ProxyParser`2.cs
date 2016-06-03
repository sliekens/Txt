using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public class ProxyParser<TElement, TResult> : IParser<TElement, TResult>
        where TElement : Element
    {
        private IParser<TElement, TResult> innerParser;

        public void Initialize([NotNull] IParser<TElement, TResult> parser)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }
            if (innerParser != null)
            {
                throw new InvalidOperationException(
                    "Initialize(ProxyParser`2) has already been called. Changing the parser after it has been initialized is not allowed.");
            }
            innerParser = parser;
        }

        public TResult Parse(TElement value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (innerParser == null)
            {
                throw new InvalidOperationException("Initialize(IParser`2) has never been called.");
            }
            return innerParser.Parse(value);
        }
    }
}
