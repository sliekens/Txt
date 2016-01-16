// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Provides the base class for lexers. A lexer is a class that matches symbols from a data source against a grammar rule to produce grammar elements. Each class that extends the <see cref="Lexer{TElement}" /> class corresponds to a singe grammar rule. For complex grammars with many grammar rules, multiple lexers work together to convert the input text to a parse tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx
{
    /// <summary>
    ///     Provides the base class for lexers. A lexer is a class that matches symbols from a data source against a
    ///     grammar rule to produce grammar elements. Each class that extends the <see cref="Lexer{TElement}" /> class
    ///     corresponds to a singe grammar rule. For complex grammars with many grammar rules, multiple lexers work together to
    ///     convert the input text to a parse tree.
    /// </summary>
    /// <typeparam name="TElement">The type of the element that represents the lexer rule.</typeparam>
    /// <remarks>
    ///     <para>The terms "lexer rule" and "grammar rule" are used interchangeably.</para>
    ///     <para>
    ///         Notes to inheritors.
    ///         The name of grammar rules are case insensitive.
    ///         At minimum, you must provide an implementation for the <see cref="Read" /> method.
    ///         There are a number of conventions that you should follow.
    ///         If the value of <see cref="ITextScanner.EndOfInput" /> is <c>true</c> and the grammar rule is not optional, you
    ///         should immediately return <c>false</c>.
    ///         Do not throw any exceptions in <see cref="Read" />.
    ///         Lexer classes should be sealed.
    ///         Re-use lexer classes for lexer rules that reference other lexer rules.
    ///     </para>
    /// </remarks>
    public abstract class Lexer<TElement> : ILexer<TElement>
        where TElement : Element
    {
        public abstract ReadResult<TElement> Read(ITextScanner scanner);

        ReadResult<Element> ILexer.ReadElement(ITextScanner scanner)
        {
            var result = Read(scanner);
            if (!result.Success)
            {
                return ReadResult<Element>.FromError(result.Error);
            }
            return ReadResult<Element>.FromResult(result.Element);
        }
    }
}
