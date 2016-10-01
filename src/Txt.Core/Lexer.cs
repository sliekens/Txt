using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

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
            return new LexerImpl(this, scanner, context);
        }

        [NotNull]
        [ItemNotNull]
        protected abstract IEnumerable<TElement> ReadImpl(
            [NotNull] ITextScanner scanner,
            [NotNull] ITextContext context);

        private class LexerImpl : IEnumerable<TElement>
        {
            [NotNull]
            private readonly ITextContext context;

            [NotNull]
            private readonly Lexer<TElement> impl;

            [NotNull]
            private readonly ITextScanner scanner;

            public LexerImpl(
                [NotNull] Lexer<TElement> impl,
                [NotNull] ITextScanner scanner,
                [NotNull] ITextContext context)
            {
                this.impl = impl;
                this.scanner = scanner;
                this.context = context;
            }

            public IEnumerator<TElement> GetEnumerator()
            {
                return new ReadImpl(impl, scanner, context);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new ReadImpl(impl, scanner, context);
            }

            private class ReadImpl : IEnumerator<TElement>
            {
                [NotNull]
                private readonly ITextContext context;

                [NotNull]
                private readonly Lexer<TElement> lexer;

                [NotNull]
                private readonly ITextScanner scanner;

                private bool initialized;

                private IEnumerator<TElement> inner;

                private long start;

                public ReadImpl(
                    [NotNull] Lexer<TElement> lexer,
                    [NotNull] ITextScanner scanner,
                    [NotNull] ITextContext context)
                {
                    if (lexer == null)
                    {
                        throw new ArgumentNullException(nameof(lexer));
                    }
                    if (scanner == null)
                    {
                        throw new ArgumentNullException(nameof(scanner));
                    }
                    if (context == null)
                    {
                        throw new ArgumentNullException(nameof(context));
                    }
                    this.lexer = lexer;
                    this.scanner = scanner;
                    this.context = context;
                }

                public TElement Current { get; private set; }

                object IEnumerator.Current => Current;

                public void Dispose()
                {
                    if (initialized)
                    {
                        Debug.Assert(inner != null, "inner != null");
                        inner.Dispose();
                        scanner.StopRecording();
                    }
                }

                public bool MoveNext()
                {
                    if (!initialized)
                    {
                        start = scanner.StartRecording();
                        inner = lexer.ReadImpl(scanner, context).GetEnumerator();
                        initialized = true;
                    }
                    if (scanner.Offset != context.Offset)
                    {
                        scanner.Seek(context.Offset);
                    }
                    if (!inner.MoveNext())
                    {
                        if (scanner.Offset != context.Offset)
                        {
                            scanner.Seek(context.Offset);
                        }
                        return false;
                    }
                    Current = inner.Current;
                    return true;
                }

                public void Reset()
                {
                    if (!initialized)
                    {
                        return;
                    }
                    Debug.Assert(inner != null, "inner != null");
                    inner.Dispose();
                    if (scanner.Offset != start)
                    {
                        scanner.Seek(start);
                    }
                    initialized = false;
                }
            }
        }
    }
}
