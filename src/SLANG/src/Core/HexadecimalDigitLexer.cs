// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class HexadecimalDigitLexer : Lexer<HexadecimalDigit>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<Digit> digitLexer;

        public HexadecimalDigitLexer()
            : this(new DigitLexer())
        {
        }

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

            foreach (var c in new[] { "A", "B", "C", "D", "E", "F" })
            {
                Element letter;
                if (TryReadTerminal(scanner, c, out letter))
                {
                    element = new HexadecimalDigit(letter, context);
                    return true;
                }
            }

            element = default(HexadecimalDigit);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.digitLexer != null);
        }
    }
}