// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Provides the interface for types that process the lexical syntax of a language.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx
{
    using System;

    /// <summary>Provides the interface for types that process the lexical syntax of a language.</summary>
    /// <typeparam name="TElement">The type of the element that represents the lexer rule.</typeparam>
    public interface ILexer<TElement> : ILexer
        where TElement : Element
    {
        /// <summary>Reads the next element.</summary>
        /// <param name="scanner">
        ///     The scanner object that provides text symbols as well as contextual information about the text
        ///     source.
        /// </param>
        /// <param name="previousElementOrNull"></param>
        /// <exception cref="InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="FormatException">
        ///     The scanner returned an unexpected element, or unexpectedly reached the
        ///     end of the text source.
        /// </exception>
        /// <returns>The next available element.</returns>
        TElement Read(ITextScanner scanner, Element previousElementOrNull);

        /// <summary>Attempts to read the next element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">
        ///     The scanner object that provides text symbols as well as contextual information about the text
        ///     source.
        /// </param>
        /// <param name="previousElementOrNull"></param>
        /// <param name="element">
        ///     When this method returns, contains the next available element, or a <c>null</c> reference, depending
        ///     on whether the return value indicates success.
        /// </param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        bool TryRead(ITextScanner scanner, Element previousElementOrNull, out TElement element);
    }
}