// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Provides the base class for lexers. A lexer is a class that matches symbols from a data source against a grammar rule to produce grammar elements. Each class that extends the <see cref="Lexer{TElement}" /> class corresponds to a singe grammar rule. For complex grammars with many grammar rules, multiple lexers work together to convert the input text to a parse tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
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
    /// <remarks>
    ///     Notes to inheritors.
    ///     At minimum, you must provide an implementation for the <see cref="ReadImpl" /> method.
    ///     There are conventions that you should follow.
    ///     Do not throw exceptions.
    ///     Lexer classes should be sealed.
    /// </remarks>
    public abstract class Lexer<TElement> : ILexer<TElement>
        where TElement : Element
    {
        public abstract ReadResult<TElement> ReadImpl([NotNull] ITextScanner scanner);

        /// <summary>Attempts to read the next element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">
        ///     The scanner object that provides text symbols as well as contextual information about the text
        ///     source.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="scanner" /> is a null reference.</exception>
        /// <exception cref="ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns>
        ///     A value container that contains the next available element, or a <c>null</c> reference, depending on whether
        ///     the return value indicates success.
        /// </returns>
        public ReadResult<TElement> Read(ITextScanner scanner)
        {
            return ReadImpl(scanner);
        }

        ReadResult<Element> ILexer.ReadElement(ITextScanner scanner)
        {
            var result = ReadImpl(scanner);
            if (result.Success)
            {
                return ReadResult<Element>.FromResult(result.Element);
            }
            return ReadResult<Element>.FromSyntaxError(result.Error);
        }
    }
}
