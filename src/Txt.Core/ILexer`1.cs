using System.Collections.Generic;
using JetBrains.Annotations;

namespace Txt.Core
{
    /// <summary>Provides the interface for types that process the lexical syntax of a language.</summary>
    /// <typeparam name="TElement">The type of the element that represents the lexer rule.</typeparam>
    public interface ILexer<out TElement>
        where TElement : Element
    {
        /// <summary>
        ///     Iterates all possible matches for <typeparamref name="TElement" /> beginning at the specified offset.
        /// </summary>
        /// <param name="scanner">The scanner object to read from.</param>
        /// <param name="context">The context object that describes the offset to begin reading at.</param>
        /// <remarks>
        ///     The implementation MUST call <see cref="ITextScanner.StartRecording" /> upon entry.
        ///     The implementation MUST <see cref="ITextScanner.Seek" /> to the offset specified by <paramref name="context" />
        ///     before every iteration.
        ///     The implementation MUST <see cref="ITextScanner.Seek" /> to the offset specified by <paramref name="context" /> at
        ///     the end of iterations in case of a partial match.
        ///     The implementation MUST NOT change current offset at the end of iterations in case of a successful match.
        ///     The implementation MUST call <see cref="ITextScanner.StopRecording" /> in a finally-block immediately before
        ///     returning.
        /// </remarks>
        /// <returns>A collection of all possible matches.</returns>
        [NotNull]
        [ItemNotNull]
        IEnumerable<TElement> Read([NotNull] ITextScanner scanner, [NotNull] ITextContext context);
    }
}
