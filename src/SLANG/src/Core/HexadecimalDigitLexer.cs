// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;
    using System.Diagnostics;

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
            if (digitLexer == null)
            {
                throw new ArgumentNullException("digitLexer", "Precondition: digitLexer != null");
            }

            this.digitLexer = digitLexer;
        }

        protected override HexadecimalDigit CreateInstance1(Digit element)
        {
            return new HexadecimalDigit(element, 1);
        }

        protected override HexadecimalDigit CreateInstance2(Element element)
        {
            return new HexadecimalDigit(element, 2);
        }

        protected override HexadecimalDigit CreateInstance3(Element element)
        {
            return new HexadecimalDigit(element, 3);
        }

        protected override HexadecimalDigit CreateInstance4(Element element)
        {
            return new HexadecimalDigit(element, 4);
        }

        protected override HexadecimalDigit CreateInstance5(Element element)
        {
            return new HexadecimalDigit(element, 5);
        }

        protected override HexadecimalDigit CreateInstance6(Element element)
        {
            return new HexadecimalDigit(element, 6);
        }

        protected override HexadecimalDigit CreateInstance7(Element element)
        {
            return new HexadecimalDigit(element, 7);
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
    }
}