using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>Provides the interface for factory classes that create lexers for a terminal value.</summary>
    public interface ITerminalLexerFactory
    {
        /// <summary>
        ///     Initializes a new instance of a class that implements the <see cref="ILexer{TElement}" /> interface for a given
        ///     terminal value. A parameter specifies the rules for the comparison.
        /// </summary>
        /// <param name="terminal">The terminal value.</param>
        /// <param name="comparer"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="terminal" /> or <paramref name="comparer" /> is a null
        ///     reference.
        /// </exception>
        /// <returns>>An instance of a class that implements <see cref="ILexer{TElement}" /> for the given terminal value.</returns>
        [NotNull]
        ILexer<Terminal> Create([NotNull] string terminal, [NotNull] IEqualityComparer<string> comparer);
    }
}
