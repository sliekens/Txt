// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Provides the interface for types that process the lexical syntax of a language.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG
{
    using System.Diagnostics.Contracts;

    /// <summary>Provides the interface for types that process the lexical syntax of a language.</summary>
    /// <typeparam name="TElement">The type of the element that represents the lexer rule.</typeparam>
    [ContractClass(typeof(ContractClassForILexer<>))]
    public interface ILexer<TElement>
        where TElement : Element
    {
        /// <summary>Gets the name of the lexer rule that is represented by the type of <typeparamref name="TElement"/>.</summary>
        /// <remarks>Rule names are case insensitive. The names "rulename", "Rulename", "RULENAME", and "rUlENamE" all refer to the same rule.</remarks>
        string RuleName { get; }

        /// <summary>Reads the next element.</summary>
        /// <param name="scanner"></param>
        /// <exception cref="T:SLANG.SyntaxErrorException"></exception>
        /// <returns>The next available element.</returns>
        TElement Read(ITextScanner scanner);

        /// <summary>Reads the next element. A return value indicates whether a element was available.</summary>
        /// <param name="scanner"></param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending
        /// on whether the return value indicates success.</param>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        bool TryRead(ITextScanner scanner, out TElement element);
    }
}