using JetBrains.Annotations;

namespace Txt.Core
{
    public abstract class LexerFactory<T> : ILexerFactory<T>
        where T : Element
    {
        [NotNull]
        private readonly SingletonLexerFactory<T> singleton;

        protected LexerFactory()
        {
            singleton = new SingletonLexerFactory<T>(this);
        }

        /// <inheritdoc />
        public abstract ILexer<T> Create();

        public ILexerFactory<T> Singleton()
        {
            return singleton;
        }
    }
}
