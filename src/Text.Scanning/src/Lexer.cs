// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    /// <summary>TODO </summary>
    /// <typeparam name="TElement"></typeparam>
    public abstract class Lexer<TElement> : ILexer<TElement>
        where TElement : Element
    {
        /// <inheritdoc />
        public virtual void PutBack(ITextScanner scanner, TElement element)
        {
            var data = element.Data;
            for (var i = data.Length - 1; i >= 0; i--)
            {
                scanner.PutBack(data[i]);
            }
        }

        /// <inheritdoc />
        public abstract TElement Read(ITextScanner scanner);

        /// <inheritdoc />
        public abstract bool TryRead(ITextScanner scanner, out TElement element);

        /// <summary>Utility method. Sets a specified element to its default value, and returns <c>false</c>.</summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected static bool Default(out TElement element)
        {
            element = default(TElement);
            return false;
        }
    }
}