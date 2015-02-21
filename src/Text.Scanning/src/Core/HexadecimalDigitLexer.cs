// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>TODO </summary>
    public class HexadecimalDigitLexer : Lexer<HexadecimalDigit>
    {
        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<Digit> digitLexer;

        /// <summary>TODO </summary>
        public HexadecimalDigitLexer()
            : this(new DigitLexer())
        {
        }

        /// <summary>TODO </summary>
        /// <param name="digitLexer">TODO </param>
        public HexadecimalDigitLexer(ILexer<Digit> digitLexer)
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
        }

        /// <inheritdoc />
        public override HexadecimalDigit Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            HexadecimalDigit element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'HEXDIG'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out HexadecimalDigit element)
        {
            if (scanner.EndOfInput)
            {
                element = default(HexadecimalDigit);
                return false;
            }

            var context = scanner.GetContext();
            Digit digit;
            if (this.digitLexer.TryRead(scanner, out digit))
            {
                element = new HexadecimalDigit(digit, context);
                return true;
            }

            // A-F
            for (var c = 'A'; c <= 'F'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new HexadecimalDigit(c, context);
                    return true;
                }
            }

            // a-f
            for (var c = 'a'; c <= 'f'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new HexadecimalDigit(c, context);
                    return true;
                }
            }

            element = default(HexadecimalDigit);
            return false;
        }

        /// <summary>TODO </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.digitLexer != null);
        }
    }
}