// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   TODO
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>TODO </summary>
    public class HexadecimalDigitLexer : Lexer<HexadecimalDigit>
    {
        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<Digit> digitLexer;

        /// <summary>Initializes a new instance of the <see cref="HexadecimalDigitLexer"/> class.</summary>
        public HexadecimalDigitLexer()
            : this(new DigitLexer())
        {
        }

        /// <summary>Initializes a new instance of the <see cref="HexadecimalDigitLexer"/> class.</summary>
        /// <param name="digitLexer">TODO The digit lexer.</param>
        public HexadecimalDigitLexer(ILexer<Digit> digitLexer)
            : base("HEXDIG")
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
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