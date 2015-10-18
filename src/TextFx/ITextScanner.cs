// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITextScanner.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx
{
    using System;
    using System.Threading.Tasks;

    /// <summary>Provides the interface for types that scan text with 1 character of lookahead and unlimited backtracking.</summary>
    public interface ITextScanner : ITextContext, IDisposable
    {
        /// <summary>Gets or sets a value indicating whether the end of the input has been reached.</summary>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        bool EndOfInput { get; }

        /// <summary>Gets a snapshot of the current context. The return value is immutable.</summary>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns>The current context.</returns>
        /// <remarks>
        ///     Notes to inheritors.
        ///     The return value MUST be immutable. You MUST NOT return 'this' from this method, because '(ITextContext)this' is
        ///     not immutable.
        /// </remarks>
        ITextContext GetContext();

        /// <summary>Prepends the given text to the input stream.</summary>
        /// <param name="s">The text to put back.</param>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        void Unread(string s);

        /// <summary>Prepends the given text to the input stream.</summary>
        /// <param name="s">The text to put back.</param>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        Task UnreadAsync(string s);

        /// <summary>Compares the given character to the next available character and advances the scanner's position if there is a match. This method performs a case-sensitive comparison using ordinal rules.</summary>
        /// <param name="c">The character to compare to the next available character.</param>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns>A value container that contains the next available character and another value indicating whether it matches the given character.</returns>
        MatchResult TryMatch(char c);

        /// <summary>Compares the given string to the next available string and advances the scanner's position if there is a match. This method performs a case-sensitive comparison using ordinal rules.</summary>
        /// <param name="s">The string to compare to the next available string.</param>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns>A value container that contains the next available string and another value indicating whether it matches the given string.</returns>
        MatchResult TryMatch(string s);

        /// <summary>Compares the given string to the next available string and advances the scanner's position if there is a match. A parameter specifies the comparison type.</summary>
        /// <param name="s">The string to compare to the next available string.</param>
        /// <param name="comparer"></param>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns>A value container that contains the next available string and another value indicating whether it matches the given string.</returns>
        MatchResult TryMatch(string s, StringComparer comparer);
    }
}