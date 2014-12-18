﻿namespace Text.Scanning
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>Provides the interface for types that scan text with 1 character of lookahead and no backtracking.</summary>
    [ContractClass((typeof(ContractClassForITextScanner)))]
    public interface ITextScanner : ITextContext, IDisposable
    {
        /// <summary>Gets or sets a value indicating whether the end of the input has been reached.</summary>
        bool EndOfInput { get; }

        /// <summary>Gets the next available character without consuming it.</summary>
        char? NextCharacter { get; }

        /// <summary>Gets the current context. Implementers: do not return 'this'. The return value MUST be immutable.</summary>
        /// <returns>The current context.</returns>
        ITextContext GetContext();

        /// <summary>Prepends the given character to the input stream, effectively rewinding the scanner.</summary>
        void PutBack(char c);

        /// <summary>Consumes a character and advances the scanner's position to the next character.</summary>
        /// <returns><c>true</c> if the next character is available; otherwise, <c>false</c>.</returns>
        bool Read();

        /// <summary>Compares the given character to the next available character. If there is a match, the character is consumed.</summary>
        /// <exception cref="T:System.InvalidOperationException">There is no next character available. This occurs when <see cref="Read"/> has never been called, or when <see cref="EndOfInput"/> is <c>true</c>.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        bool TryMatch(char c);
    }
}