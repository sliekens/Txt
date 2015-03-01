// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITextScanner.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>Provides the interface for types that scan text with 1 character of lookahead and unlimited backtracking.</summary>
    [ContractClass(typeof(ContractClassForITextScanner))]
    public interface ITextScanner : ITextContext, IDisposable
    {
        /// <summary>Gets or sets a value indicating whether the end of the input has been reached.</summary>
        /// <exception cref="T:System.ObjectDisposedException">The current text scanner is closed.</exception>
        bool EndOfInput { get; }

        /// <summary>Gets the next available character without consuming it.</summary>
        /// <exception cref="T:System.ObjectDisposedException">The current text scanner is closed.</exception>
        char? NextCharacter { get; }

        /// <summary>Gets a snapshot of the current context. The return value is immutable.</summary>
        /// <exception cref="T:System.ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns>The current context.</returns>
        /// <remarks>
        /// Notes to inheritors.
        /// The return value MUST be immutable. You MUST NOT return 'this' from this method, because '(ITextContext)this' is not immutable.
        /// </remarks>
        ITextContext GetContext();

        /// <summary>Prepends the given character to the input stream.</summary>
        /// <param name="c">The character to put back.</param>
        /// <exception cref="T:System.ObjectDisposedException">The current text scanner is closed.</exception>
        void PutBack(char c);

        /// <summary>Prepends the given text to the input stream.</summary>
        /// <param name="s">The text to put back.</param>
        /// <exception cref="T:System.ObjectDisposedException">The current text scanner is closed.</exception>
        void PutBack(string s);

        /// <summary>Consumes a character and advances the scanner's position to the next character.</summary>
        /// <exception cref="T:System.ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns><c>true</c> if the next character is available; otherwise, <c>false</c>.</returns>
        bool Read();

        /// <summary>Compares the given character to the next available character. If there is a match, the character is consumed.</summary>
        /// <param name="c">The character to compare to the next available character.</param>
        /// <exception cref="T:System.InvalidOperationException">There is no next character available. This occurs when<see cref="Read"/> has never been called, or when <see cref="EndOfInput"/> is <c>true</c>.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        bool TryMatch(char c);
    }
}