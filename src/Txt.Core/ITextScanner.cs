using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Txt.Core
{
    public interface ITextScanner : ITextSource
    {
        /// <summary>Gets a snapshot of the current context. The return value is immutable.</summary>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns>The current context.</returns>
        /// <remarks>
        ///     Notes to inheritors.
        ///     The return value MUST be immutable. You MUST NOT return 'this' from this method, because '(ITextContext)this' is
        ///     not immutable.
        /// </remarks>
        [NotNull]
        ITextContext GetContext();

        /// <summary>
        ///     Compares the given character to the next available character and advances the scanner's position if there is a
        ///     match. This method performs a case-sensitive ordinal comparison.
        /// </summary>
        /// <param name="c">The character to compare to the next available character.</param>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns>
        ///     A value container that contains the next available character and another value indicating whether it matches
        ///     the given character.
        /// </returns>
        ScanResult TryMatch(char c);

        /// <summary>
        ///     Advances the scanner's position if the next available character matches the conditions defined by the specified
        ///     predicate.
        /// </summary>
        /// <param name="predicate">The <see cref="Predicate{T}" /> that defines the conditions of the character to match.</param>
        /// <returns>
        ///     A value container that contains the next available character and another value indicating whether it matches
        ///     the conditions defined by the predicate.
        /// </returns>
        ScanResult TryMatch(Predicate<char> predicate);

        /// <summary>
        ///     Compares the given string to the next available string and advances the scanner's position if there is a
        ///     match. This method performs a case-sensitive ordinal comparison.
        /// </summary>
        /// <param name="s">The string to compare to the next available string.</param>
        /// <exception cref="ArgumentNullException"><paramref name="s" /> is a null reference.</exception>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns>
        ///     A value container that contains the next available string and another value indicating whether it matches the
        ///     given string.
        /// </returns>
        ScanResult TryMatch([NotNull] string s);

        /// <summary>
        ///     Compares the given string to the next available string and advances the scanner's position if there is a
        ///     match. A parameter specifies the rules for the comparison.
        /// </summary>
        /// <param name="s">The string to compare to the next available string.</param>
        /// <param name="comparer">An object that checks two strings for equality.</param>
        /// <exception cref="ArgumentNullException"><paramref name="s" /> or <paramref name="comparer" /> is a null reference.</exception>
        /// <exception cref="ObjectDisposedException">The current text scanner is closed.</exception>
        /// <returns>
        ///     A value container that contains the next available string and another value indicating whether it matches the
        ///     given string.
        /// </returns>
        ScanResult TryMatch([NotNull] string s, [NotNull] IEqualityComparer<string> comparer);
    }
}
