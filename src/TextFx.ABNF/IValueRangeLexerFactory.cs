﻿namespace TextFx.ABNF
{
    using System;
    using System.Text;
    using JetBrains.Annotations;

    /// <summary>Provides the interface for factory classes that create lexers for a range of alternative values.</summary>
    public interface IValueRangeLexerFactory
    {
        /// <summary>
        ///     Initializes a new instance of a class that implements the <see cref="ILexer{TElement}" /> interface with a
        ///     specified lower and upper bound, both inclusive.
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range of alternatives (inclusive).</param>
        /// <param name="upperBound">The upper bound of the range of alternatives (inclusive).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The value of <paramref name="upperBound" /> is smaller than the value of
        ///     <paramref name="lowerBound" />.
        /// </exception>
        ILexer<Terminal> Create(char lowerBound, char upperBound);

        /// <summary>
        ///     Initializes a new instance of a class that implements the <see cref="ILexer{TElement}" /> interface with a
        ///     specified lower and upper bound, both inclusive.
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range of alternatives (inclusive).</param>
        /// <param name="upperBound">The upper bound of the range of alternatives (inclusive).</param>
        /// <param name="encoding">The encoding that is used to convert from <see cref="int" /> to <see cref="char" />.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="encoding" /> is a null reference.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The value of <paramref name="upperBound" /> is smaller than the value of
        ///     <paramref name="lowerBound" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     One or more values in the range are not valid in the specified encoding.
        /// </exception>
        ILexer<Terminal> Create(int lowerBound, int upperBound, [NotNull] Encoding encoding);
    }
}
