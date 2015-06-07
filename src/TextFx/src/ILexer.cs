namespace TextFx
{
    using System;

    /// <summary>Provides the base interface for types that process the lexical syntax of a language.</summary>
    public interface ILexer
    {
        /// <summary>Reads the next element.</summary>
        /// <param name="scanner">
        ///     The scanner object that provides text symbols as well as contextual information about the text
        ///     source.
        /// </param>
        /// <exception cref="InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="FormatException">
        ///     The scanner returned an unexpected element, or unexpectedly reached the
        ///     end of the text source.
        /// </exception>
        /// <returns>The next available element.</returns>
        Element ReadElement(ITextScanner scanner);

        /// <summary>Attempts to read the next element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">
        ///     The scanner object that provides text symbols as well as contextual information about the text
        ///     source.
        /// </param>
        /// <param name="element">
        ///     When this method returns, contains the next available element, or a <c>null</c> reference, depending
        ///     on whether the return value indicates success.
        /// </param>
        /// <exception cref="InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        bool TryReadElement(ITextScanner scanner, out Element element);
    }
}