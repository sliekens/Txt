namespace Text.Scanning
{
    using System.Diagnostics.Contracts;

    /// <summary>Provides the interface for types that process the lexical syntax of a language.</summary>
    /// <typeparam name="TElement">The type of elements produced by the lexer.</typeparam>
    [ContractClass((typeof(ContractClassForILexer<>)))]
    public interface ILexer<TElement>
        where TElement : Element
    {
        // TODO: review this method. This method accepts any TElement, in any order, regardless of the source data. This could be bad.
        /// <summary>Puts a element back in the source data.</summary>
        /// <param name="scanner"></param>
        /// <param name="element">The element to put back.</param>
        void PutBack(ITextScanner scanner, TElement element);

        /// <summary>Reads the next element.</summary>
        /// <param name="scanner"></param>
        /// <exception cref="T:Text.Scanning.SyntaxErrorException"></exception>
        /// <returns>The next available element.</returns>
        TElement Read(ITextScanner scanner);

        /// <summary>Reads the next element. A return value indicates whether a element was available.</summary>
        /// <param name="scanner"></param>
        /// <param name="element">
        /// When this method returns, contains the next available element, or a <c>null</c> reference, depending
        /// on whether the return value indicates success.
        /// </param>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        bool TryRead(ITextScanner scanner, out TElement element);
    }
}