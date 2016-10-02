using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public class LexerFactoryAdapter<T> : ILexerFactory<T>
        where T : Element
    {
        private readonly ILexer<T> instance;

        public LexerFactoryAdapter([NotNull] ILexer<T> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            this.instance = instance;
        }

        public ILexer<T> Create()
        {
            return instance;
        }
    }
}
