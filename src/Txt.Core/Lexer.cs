using System;
using System.Collections.Generic;
using System.Linq;

namespace Txt.Core
{
    /// <summary>
    ///     Provides the base class for lexers. A lexer is a class that matches symbols from a data source against a
    ///     grammar rule to produce grammar elements. Each class that extends the <see cref="Lexer{TElement}" /> class
    ///     corresponds to a singe grammar rule. For complex grammars with many grammar rules, multiple lexers work together to
    ///     convert the input text to a parse tree.
    /// </summary>
    /// <typeparam name="TElement">The type of the element that represents the lexer rule.</typeparam>
    public abstract class Lexer<TElement> : ILexer<TElement>
        where TElement : Element
    {
        protected readonly IEnumerable<TElement> Empty = Enumerable.Empty<TElement>();

        public TElement Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var offset = scanner.StartRecording();
            TElement candidate = null;
            try
            {
                var context = scanner.GetContext();
                foreach (var element in ReadImpl(scanner, context))
                {
                    if (candidate == null)
                    {
                        candidate = element;
                    }
                    else if (element.Text.Length > candidate.Text.Length)
                    {
                        candidate = element;
                    }
                }
                if (candidate == null)
                {
                    return null;
                }
                scanner.Seek(offset + candidate.Text.Length);
            }
            finally
            {
                scanner.StopRecording();
            }
            return candidate;
        }

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
        public IEnumerable<TElement> Read(ITextScanner scanner, ITextContext context)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            scanner.StartRecording();
            try
            {
                scanner.Seek(context.Offset);
                return ReadImpl(scanner, context);
            }
            finally
            {
                scanner.StopRecording();
            }
        }

        public abstract IEnumerable<TElement> ReadImpl(ITextScanner scanner, ITextContext context);
    }
}
