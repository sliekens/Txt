using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public class LexerFactoryAdapter<T> : LexerFactory<T>
        where T : Element
    {
        [NotNull]
        private readonly ILexer<T> instance;

        public LexerFactoryAdapter([NotNull] ILexer<T> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            this.instance = instance;
        }

        public override ILexer<T> Create()
        {
            return instance;
        }
    }
}
