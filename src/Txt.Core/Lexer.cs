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
        public IReadResult<TElement> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var context = scanner.GetContext();
            var result = ReadImpl(scanner, context);
            if (result == null)
            {
                throw new InvalidOperationException($"{GetType()}.ReadImpl returned null.");
            }
            if (result.IsSuccess)
            {
                if (scanner.Offset != context.Offset + result.Element.Text.Length)
                {
                    throw new InvalidOperationException(
                        $"{GetType()}.ReadImpl returned a success result, but the text length is not equal to the difference between the old offset and the new offset in the text source. Calculated offset: {context.Offset} to {scanner.Offset} (length: {scanner.Offset - context.Offset}). The reported length is: {result.Element.Text.Length}. Reported offset: {result.Element.Context.Offset}. Calculated end offset: {result.Element.Context.Offset + result.Element.Text.Length}.");
                }
            }
            else
            {
                if (scanner.Offset != context.Offset)
                {
                    throw new InvalidOperationException(
                        $"{GetType()}.ReadImpl returned an error result, but the new offset is not equal to the old offset in the text source. Old offset: {context.Offset}. New offset: {scanner.Offset}.");
                }
            }
            return result;
        }

        /// <summary>
        ///     Provides the implementation of the lexer rule. Notes to implementers: the return value indiocates whether the
        ///     element was available.
        /// </summary>
        /// <param name="scanner"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected abstract IReadResult<TElement> ReadImpl(
            [NotNull] ITextScanner scanner,
            [NotNull] ITextContext context);
    }
}
