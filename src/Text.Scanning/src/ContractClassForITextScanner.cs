// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContractClassForITextScanner.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>TODO </summary>
    [ContractClassFor(typeof(ITextScanner))]
    internal abstract class ContractClassForITextScanner : ITextScanner
    {
        /// <summary>TODO </summary>
        /// <exception cref="NotImplementedException"></exception>
        public bool EndOfInput
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>TODO </summary>
        /// <exception cref="NotImplementedException"></exception>
        public char? NextCharacter
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>TODO </summary>
        public abstract int Offset { get; }

        /// <summary>TODO </summary>
        public abstract void Dispose();

        /// <summary>TODO </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ITextContext GetContext()
        {
            Contract.Ensures(Contract.Result<ITextContext>() != null);
            throw new NotImplementedException();
        }

        /// <summary>TODO </summary>
        /// <param name="c">TODO </param>
        /// <exception cref="NotImplementedException"></exception>
        public void PutBack(char c)
        {
            // Contract.Ensures(Contract.OldValue(this.Offset) == this.Offset + 1 || this.Offset == 0);
            throw new NotImplementedException();
        }

        /// <summary>TODO </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Read()
        {
            // Contract.Ensures(Contract.OldValue(this.Offset) == this.Offset - 1 || this.EndOfInput);
            throw new NotImplementedException();
        }

        /// <summary>TODO </summary>
        /// <param name="c">TODO </param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool TryMatch(char c)
        {
            // Contract.Ensures(c != this.NextCharacter || Contract.OldValue(this.Offset) == this.Offset - 1 || this.EndOfInput);
            throw new NotImplementedException();
        }
    }
}