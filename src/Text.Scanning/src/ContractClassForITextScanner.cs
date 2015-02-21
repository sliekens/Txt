// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContractClassForITextScanner.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   The contract class for i text scanner.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>The contract class for i text scanner.</summary>
    [ContractClassFor(typeof(ITextScanner))]
    internal abstract class ContractClassForITextScanner : ITextScanner
    {
        /// <summary>Gets a value indicating whether end of input.</summary>
        /// <exception cref="NotImplementedException"></exception>
        public bool EndOfInput
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>Gets the next character.</summary>
        /// <exception cref="NotImplementedException"></exception>
        public char? NextCharacter
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>Gets the offset.</summary>
        public abstract int Offset { get; }

        /// <summary>The dispose.</summary>
        public abstract void Dispose();

        /// <summary>The get context.</summary>
        /// <returns>The <see cref="ITextContext"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public ITextContext GetContext()
        {
            Contract.Ensures(Contract.Result<ITextContext>() != null);
            throw new NotImplementedException();
        }

        /// <summary>The put back.</summary>
        /// <param name="c">The c.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void PutBack(char c)
        {
            // Contract.Ensures(Contract.OldValue(this.Offset) == this.Offset + 1 || this.Offset == 0);
            throw new NotImplementedException();
        }

        /// <summary>The read.</summary>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Read()
        {
            // Contract.Ensures(Contract.OldValue(this.Offset) == this.Offset - 1 || this.EndOfInput);
            throw new NotImplementedException();
        }

        /// <summary>The try match.</summary>
        /// <param name="c">The c.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool TryMatch(char c)
        {
            // Contract.Ensures(c != this.NextCharacter || Contract.OldValue(this.Offset) == this.Offset - 1 || this.EndOfInput);
            throw new NotImplementedException();
        }
    }
}