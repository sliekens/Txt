// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Provides the interface for types that process the lexical syntax of a language.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System.Diagnostics.Contracts;

    /// <summary>Provides the interface for types that process the lexical syntax of a language.</summary>
    /// <typeparam name="TToken">The type of tokens produced by the lexer.</typeparam>
    [ContractClass(typeof(ContractClassForILexer<>))]
    public interface ILexer<TToken>
        where TToken : Token
    {
        // TODO: review this method. This method accepts any TToken, in any order, regardless of the source data. This could be bad.
        /// <summary>Puts a token back in the source data.</summary>
        /// <param name="scanner"></param>
        /// <param name="token">The token to put back.</param>
        void PutBack(ITextScanner scanner, TToken token);

        /// <summary>Reads the next token.</summary>
        /// <param name="scanner"></param>
        /// <exception cref="T:Text.Scanning.SyntaxErrorException"></exception>
        /// <returns>The next available token.</returns>
        TToken Read(ITextScanner scanner);

        /// <summary>Reads the next token. A return value indicates whether a token was available.</summary>
        /// <param name="scanner"></param>
        /// <param name="token">When this method returns, contains the next available token, or a <c>null</c> reference, depending
        /// on whether the return value indicates success.</param>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        bool TryRead(ITextScanner scanner, out TToken token);
    }
}