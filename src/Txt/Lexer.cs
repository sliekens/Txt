// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Provides the base class for lexers. A lexer is a class that matches symbols from a data source against a grammar rule to produce grammar elements. Each class that extends the <see cref="Lexer{TElement}" /> class corresponds to a singe grammar rule. For complex grammars with many grammar rules, multiple lexers work together to convert the input text to a parse tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Txt
{
    /// <summary>
    ///     Provides the base class for lexers. A lexer is a class that matches symbols from a data source against a
    ///     grammar rule to produce grammar elements. Each class that extends the <see cref="Lexer{TElement}" /> class
    ///     corresponds to a singe grammar rule. For complex grammars with many grammar rules, multiple lexers work together to
    ///     convert the input text to a parse tree.
    /// </summary>
    /// <typeparam name="TElement">The type of the element that represents the lexer rule.</typeparam>
    /// <remarks>
    ///         Notes to inheritors.
    ///         At minimum, you must provide an implementation for the <see cref="Read" /> method.
    ///         There are conventions that you should follow.
    ///         Do not throw exceptions.
    ///         Lexer classes should be sealed.
    /// </remarks>
    public abstract class Lexer<TElement> : ILexer<TElement>
        where TElement : Element
    {
        public abstract ReadResult<TElement> Read(ITextScanner scanner);

        ReadResult<Element> ILexer.ReadElement(ITextScanner scanner)
        {
            var result = Read(scanner);
            if (result.Success)
            {
                return ReadResult<Element>.FromResult(result.Element);
            }
            return ReadResult<Element>.FromSyntaxError(result.Error);
        }
    }
}
