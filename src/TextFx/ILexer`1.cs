﻿// --------------------------------------------------------------------------------------------------------------------
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
    using JetBrains.Annotations;

    /// <summary>Provides the interface for types that process the lexical syntax of a language.</summary>
    /// <typeparam name="TElement">The type of the element that represents the lexer rule.</typeparam>
    public interface ILexer<TElement> : ILexer
        where TElement : Element
    {
        /// <summary>Attempts to read the next element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">
        ///     The scanner object that provides text symbols as well as contextual information about the text
        ///     source.
        /// </param>
        /// <param name="previousElementOrNull">The previous element, if any.</param>
        /// <exception cref="ArgumentNullException"><paramref name="scanner" /> is a null reference.</exception>
        /// <exception cref="ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns>
        ///     A value container that contains the next available element, or a <c>null</c> reference, depending on whether
        ///     the return value indicates success.
        /// </returns>
        ReadResult<TElement> Read([NotNull] ITextScanner scanner, [CanBeNull] Element previousElementOrNull);
    }
}
