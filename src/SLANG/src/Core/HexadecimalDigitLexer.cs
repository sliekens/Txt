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

    public class HexadecimalDigitLexer : AlternativeLexer<HexadecimalDigit, Digit, Element>
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

        protected override HexadecimalDigit CreateInstance1(Digit element, ITextContext context)
        {
            return new HexadecimalDigit(element, context);
        }

        protected override HexadecimalDigit CreateInstance2(Element element, ITextContext context)
        {
            return new HexadecimalDigit(element, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out Digit element)
        {
            return this.digitLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out Element element)
        {
            foreach (var s in new[] { "A", "B", "C", "D", "E", "F" })
            {
                if (TryReadTerminal(scanner, s, out element))
                {
                    return true;
                }
            }

            element = default(Element);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.digitLexer != null);
        }
    }
}