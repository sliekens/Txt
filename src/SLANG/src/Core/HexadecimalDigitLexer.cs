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

    public class HexadecimalDigitLexer : AlternativeLexer<HexadecimalDigit, Digit, Element, Element, Element, Element, Element, Element>
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
            return new HexadecimalDigit(element, 2, context);
        }

        protected override HexadecimalDigit CreateInstance3(Element element, ITextContext context)
        {
            return new HexadecimalDigit(element, 3, context);
        }

        protected override HexadecimalDigit CreateInstance4(Element element, ITextContext context)
        {
            return new HexadecimalDigit(element, 4, context);
        }

        protected override HexadecimalDigit CreateInstance5(Element element, ITextContext context)
        {
            return new HexadecimalDigit(element, 5, context);
        }

        protected override HexadecimalDigit CreateInstance6(Element element, ITextContext context)
        {
            return new HexadecimalDigit(element, 6, context);
        }

        protected override HexadecimalDigit CreateInstance7(Element element, ITextContext context)
        {
            return new HexadecimalDigit(element, 7, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out Digit element)
        {
            return this.digitLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "A", out element);
        }

        protected override bool TryRead3(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "B", out element);
        }

        protected override bool TryRead4(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "C", out element);
        }

        protected override bool TryRead5(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "D", out element);
        }

        protected override bool TryRead6(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "E", out element);
        }

        protected override bool TryRead7(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "F", out element);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.digitLexer != null);
        }
    }
}