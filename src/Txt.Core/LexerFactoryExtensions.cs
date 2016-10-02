using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public static class LexerFactoryExtensions
    {
        public static ILexerFactory<T> Singleton<T>([NotNull] this ILexerFactory<T> instance) where T : Element
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            if (instance is SingletonLexerFactory<T>)
            {
                return instance;
            }
            return new SingletonLexerFactory<T>(instance);
        }
    }
}
