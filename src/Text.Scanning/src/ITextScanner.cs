using System;
using System.Diagnostics.Contracts;

namespace Text.Scanning
{
    /// <summary>Provides the interface for types that scan text with 1 character of lookahead and no backtracking.</summary>
    [ContractClass((typeof(ContractClassForITextScanner)))]
    public interface ITextScanner : ITextContext, IDisposable
    {
        char NextCharacter { get; }

        /// <summary>Gets or sets a value indicating whether the end of the input has been reached.</summary>
        bool EndOfInput { get; }

        /// <summary>Reads the next character and advances the character position.</summary>
        /// <returns><c>true</c> if the next character is available; otherwise, <c>false</c>.</returns>
        bool Read();

        /// <summary>Matches the given character and the next available character.</summary>
        bool TryMatch(char c);

        ITextContext GetContext();
    }
}
