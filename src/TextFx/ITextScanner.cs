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

        /// <summary>Compares the given character to the next available character. If there is a match, the character is consumed.</summary>
        /// <param name="c">The character to compare to the next available character.</param>
        /// <param name="next">The next available character.</param>
        /// <exception cref="T:System.InvalidOperationException">
        ///     There is no next character available. This occurs when <see cref="EndOfInput" /> is <c>true</c>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        bool TryMatch(char c, out char next);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="next"></param>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns></returns>
        bool TryMatch(string s, out string next);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="comparer"></param>
        /// <param name="next"></param>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns></returns>
        bool TryMatch(string s, StringComparer comparer, out string next);
    }
}